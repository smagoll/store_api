using Application.DTOs;
using Application.DTOs.Cart;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;

public class CartService : ICartService
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public CartService(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CartDto> GetCartAsync(int userId)
    {
        var cart = await _repository.GetOrCreateCartAsync(userId);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> AddToCartAsync(AddToCartDto dto)
    {
        var cart = await _repository.GetOrCreateCartAsync(dto.UserId);

        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

        if (item != null)
        {
            item.Quantity += dto.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            });
        }

        await _repository.UpdateAsync(cart);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> UpdateQuantityAsync(UpdateCartItemDto dto)
    {
        var cart = await _repository.GetOrCreateCartAsync(dto.UserId);

        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (item == null) throw new Exception("Товар не найден в корзине");

        if (dto.Quantity <= 0)
        {
            cart.Items.Remove(item);
        }
        else
        {
            item.Quantity = dto.Quantity;
        }

        await _repository.UpdateAsync(cart);
        return _mapper.Map<CartDto>(cart);
    }

    public async Task<CartDto> RemoveFromCartAsync(RemoveFromCartDto dto)
    {
        var cart = await _repository.GetOrCreateCartAsync(dto.UserId);

        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (item != null)
        {
            cart.Items.Remove(item);
        }

        await _repository.UpdateAsync(cart);
        return _mapper.Map<CartDto>(cart);
    }
}