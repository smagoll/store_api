namespace Application.DTOs.Book;

public record CreateBookDto(
    string Title,
    string Description,
    string? CoverUrl,
    List<Guid> AuthorIds,
    List<Guid>? CategoryIds
);