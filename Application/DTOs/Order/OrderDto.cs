namespace Application.DTOs;

public record OrderItemDto(
    int ProductId,
    string ProductName,
    decimal Price,
    int Quantity
);

public record OrderDto(
    int Id,
    int UserId,
    DateTime CreatedAt,
    string Status,
    decimal TotalPrice,
    List<OrderItemDto> Items
)
{
    public OrderDto(int Id, int UserId, DateTime CreatedAt, string Status, decimal TotalPrice)
        : this(Id, UserId, CreatedAt, Status, TotalPrice, new List<OrderItemDto>())
    {
    }
}