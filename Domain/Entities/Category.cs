using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Product> Products { get; set; }
}