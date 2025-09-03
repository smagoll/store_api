using Application.DTOs;
using Application.DTOs.Auth;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryTitle, opt => opt.MapFrom(src => src.Category.Title));
        
        CreateMap<User, UserDto>();
        
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryCreateDto, Category>();
    }
}