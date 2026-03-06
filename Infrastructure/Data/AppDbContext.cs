using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }
}