using Application.DTOs;
using Application.DTOs.Auth;
using Application.DTOs.Cart;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();
        CreateMap<DeleteProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();

        // User
        CreateMap<User, UserDto>();

        // Category
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();

        // Cart
        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<AddToCartDto, CartItem>();
        CreateMap<UpdateCartItemDto, CartItem>();

        // Order
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
    }
}