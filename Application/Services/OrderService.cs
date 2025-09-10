using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;

public class OrderService : IOrderService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(ICartRepository cartRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<OrderDto> CheckoutAsync(int userId)
    {
        var cart = await _cartRepository.GetOrCreateCartAsync(userId);
        if (!cart.Items.Any())
            throw new Exception("Корзина пуста");
        
        var order = new Order
        {
            UserId = userId,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow,
            Items = cart.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Product.Price
            }).ToList()
        };
        
        order.TotalPrice = order.Items.Sum(i => i.Price * i.Quantity);
        
        await _orderRepository.AddAsync(order);
        
        cart.Items.Clear();
        await _cartRepository.UpdateAsync(cart);
        
        return _mapper.Map<OrderDto>(order);
    }
}