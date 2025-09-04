namespace Application.DTOs;

public record ProductDto(int Id, string Name, decimal Price, int CategoryId);
public record CreateProductDto(string Name, decimal Price, int CategoryId);
public record DeleteProductDto(int Id);
public record UpdateProductDto(int Id, string Name, decimal Price, int CategoryId);