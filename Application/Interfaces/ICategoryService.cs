using Application.DTOs;

namespace Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
    Task<CategoryDto> GetByIdAsync(int id);
}