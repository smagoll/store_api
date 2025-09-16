namespace Application.DTOs.Cart;

// Cart DTOs
public record CartDto(int Id, int UserId, List<CartItemDto> Items);

public record CartItemDto(int ProductId, string ProductName, decimal Price, int Quantity);

// Commands / input DTOs
public record AddToCartDto(int UserId, int ProductId, int Quantity = 1);

public record UpdateCartItemDto(int UserId, int ProductId, int Quantity);

public record RemoveFromCartDto(int UserId, int ProductId);