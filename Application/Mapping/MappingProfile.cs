using Application.DTOs.Auth;
using Application.DTOs.Author;
using Application.DTOs.Book;
using Application.DTOs.Category;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // BOOK
        CreateMap<Book, BookDto>();
        CreateMap<Book, BookPreviewDto>();

        // AUTHOR
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorPreviewDto>();

        // CATEGORY
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryPreviewDto>();

        // USER
        CreateMap<User, UserDto>();
    }
}