using Application.DTOs;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        
        CreateMap<CreateProductDto, Product>();
        CreateMap<DeleteProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        
        CreateMap<User, UserDto>();
        
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();
    }
}