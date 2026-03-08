using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions opts) : DbContext(opts)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    // связи
    public DbSet<UserBook> UserBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserBook>()
            .HasIndex(x => new { x.UserId, x.BookId })
            .IsUnique();

        modelBuilder.Entity<UserBook>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(ub => ub.UserId);

        modelBuilder.Entity<UserBook>()
            .HasOne(ub => ub.Book)
            .WithMany(b => b.UserBooks)
            .HasForeignKey(ub => ub.BookId);
    }
}