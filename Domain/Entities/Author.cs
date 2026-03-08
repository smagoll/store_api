using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Author
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string? Biography { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}