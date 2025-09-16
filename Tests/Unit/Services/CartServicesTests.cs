using Application.DTOs.Cart;
using AutoMapper;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace Tests.Unit.Services;

public class CartServiceTests
{
    private readonly Mock<ICartRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CartService _service;

    public CartServiceTests()
    {
        _repositoryMock = new Mock<ICartRepository>();
        _mapperMock = new Mock<IMapper>();
        _service = new CartService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetCartAsync_ShouldReturnCartDto()
    {
        // Arrange
        var cart = new Cart { Id = 1, UserId = 1, Items = new List<CartItem>() };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.GetCartAsync(1);

        // Assert
        Assert.Equal(cartDto, result);
        _repositoryMock.Verify(r => r.GetOrCreateCartAsync(1), Times.Once);
    }

    [Fact]
    public async Task AddToCartAsync_ShouldAddNewItem_WhenItemDoesNotExist()
    {
        // Arrange
        var dto = new AddToCartDto(1, 10, 2);
        var cart = new Cart { Id = 1, UserId = 1, Items = new List<CartItem>() };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.AddToCartAsync(dto);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(10, cart.Items[0].ProductId);
        Assert.Equal(2, cart.Items[0].Quantity);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }

    [Fact]
    public async Task AddToCartAsync_ShouldIncreaseQuantity_WhenItemAlreadyExists()
    {
        // Arrange
        var dto = new AddToCartDto(1, 10, 2);
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            Items = new List<CartItem> { new CartItem { ProductId = 10, Quantity = 3 } }
        };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.AddToCartAsync(dto);

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(5, cart.Items[0].Quantity);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }

    [Fact]
    public async Task UpdateQuantityAsync_ShouldUpdate_WhenItemExists()
    {
        // Arrange
        var dto = new UpdateCartItemDto(1, 10, 5);
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            Items = new List<CartItem> { new CartItem { ProductId = 10, Quantity = 2 } }
        };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.UpdateQuantityAsync(dto);

        // Assert
        Assert.Equal(5, cart.Items[0].Quantity);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }

    [Fact]
    public async Task UpdateQuantityAsync_ShouldRemove_WhenQuantityIsZero()
    {
        // Arrange
        var dto = new UpdateCartItemDto(1, 10, 0);
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            Items = new List<CartItem> { new CartItem { ProductId = 10, Quantity = 2 } }
        };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.UpdateQuantityAsync(dto);

        // Assert
        Assert.Empty(cart.Items);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }

    [Fact]
    public async Task UpdateQuantityAsync_ShouldThrow_WhenItemNotFound()
    {
        // Arrange
        var dto = new UpdateCartItemDto(1, 10, 5);
        var cart = new Cart { Id = 1, UserId = 1, Items = new List<CartItem>() };

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.UpdateQuantityAsync(dto));
    }

    [Fact]
    public async Task RemoveFromCartAsync_ShouldRemoveItem_WhenExists()
    {
        // Arrange
        var dto = new RemoveFromCartDto(1, 10);
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            Items = new List<CartItem> { new CartItem { ProductId = 10, Quantity = 2 } }
        };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.RemoveFromCartAsync(dto);

        // Assert
        Assert.Empty(cart.Items);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }

    [Fact]
    public async Task RemoveFromCartAsync_ShouldDoNothing_WhenItemNotFound()
    {
        // Arrange
        var dto = new RemoveFromCartDto(1, 99);
        var cart = new Cart
        {
            Id = 1,
            UserId = 1,
            Items = new List<CartItem> { new CartItem { ProductId = 10, Quantity = 2 } }
        };
        var cartDto = new CartDto(1, 1, new List<CartItemDto>());

        _repositoryMock.Setup(r => r.GetOrCreateCartAsync(1)).ReturnsAsync(cart);
        _mapperMock.Setup(m => m.Map<CartDto>(cart)).Returns(cartDto);

        // Act
        var result = await _service.RemoveFromCartAsync(dto);

        // Assert
        Assert.Single(cart.Items);
        _repositoryMock.Verify(r => r.UpdateAsync(cart), Times.Once);
    }
}
