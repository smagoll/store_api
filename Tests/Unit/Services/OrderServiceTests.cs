using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace Tests.Unit.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _repositoryOrderMock;
    private readonly Mock<ICartRepository> _repositoryCartMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly IOrderService _service;

    public OrderServiceTests()
    {
        _repositoryOrderMock = new Mock<IOrderRepository>();
        _repositoryCartMock = new Mock<ICartRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new OrderService(
            _repositoryCartMock.Object,
            _repositoryOrderMock.Object,
            _mapperMock.Object
            );
    }

    [Fact]
    public async Task Checkout_ShouldThrow_WhenCartIsEmpty()
    {
        // Arrange
        var cart = new Cart();
        
        _repositoryCartMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.CheckoutAsync(1));
    }
    
    [Fact]
    public async Task Checkout_ShouldCreateOrder_WhenCartHasItems()
    {
        // Arrange
        var cart = new Cart
        {
            Id = 1,
            Items = new List<CartItem>
            {
                new  CartItem()
                {
                    ProductId = 1,
                    Quantity = 2,
                    Product = new Product{Id = 10, Price = 100}
                }
            }
        };
        
        _repositoryCartMock.Setup(r => r.GetOrCreateCartAsync(1))
            .ReturnsAsync(cart);

        _mapperMock.Setup(m => m.Map<OrderDto>(It.IsAny<Order>()))
            .Returns(new OrderDto(1, 1, DateTime.Now, string.Empty, 200, new List<OrderItemDto>()));

        // Act
        var result = await _service.CheckoutAsync(1);
        
        // Assert
        Assert.Equal(200, result.TotalPrice);
        _repositoryOrderMock.Verify(r => r.AddAsync(It.Is<Order>(o => o.TotalPrice == 200)), Times.Once);
        _repositoryCartMock.Verify(r => r.UpdateAsync(It.Is<Cart>(c => c.Items.Count == 0)), Times.Once);
    }
}