using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<Cart> GetOrCreateCartAsync(int userId)
    {
        var cart = await _dbSet
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                Items = new List<CartItem>()
            };
            await _dbSet.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        return cart;
    }
}