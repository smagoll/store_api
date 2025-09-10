using Application.DTOs.Cart;

namespace Application.Interfaces;

public interface ICartService
{
    Task<CartDto> GetCartAsync(int userId);
    Task<CartDto> AddToCartAsync(AddToCartDto dto);
    Task<CartDto> UpdateQuantityAsync(UpdateCartItemDto dto);
    Task<CartDto> RemoveFromCartAsync(RemoveFromCartDto dto);
}