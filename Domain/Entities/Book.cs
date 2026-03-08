using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Book
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public string? CoverUrl { get; set; }
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<Author> Authors { get; set; } = new List<Author>();
    
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}