namespace Application.DTOs;

public record CategoryDto(int Id, string Title);

public record CategoryCreateDto(string Title);