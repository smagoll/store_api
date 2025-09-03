using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category);
    Task<Category> GetByIdAsync(int id);
}