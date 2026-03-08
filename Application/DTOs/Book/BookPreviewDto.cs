namespace Application.DTOs.Book;

public record BookPreviewDto(
    Guid Id,
    string Title,
    string? CoverUrl
);