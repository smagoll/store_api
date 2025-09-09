using Domain.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Interfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetOrCreateCartAsync(int id);
}