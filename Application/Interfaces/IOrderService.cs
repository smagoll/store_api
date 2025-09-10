using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CheckoutAsync(int userId);
}