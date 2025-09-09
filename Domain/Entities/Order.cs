using System.ComponentModel.DataAnnotations;
using Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = new();

    public string Status { get; set; } = "Pending";

    public decimal TotalPrice { get; set; }
}

public class OrderItem
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; } // цена на момент покупки
}