using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Author
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Biography { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}