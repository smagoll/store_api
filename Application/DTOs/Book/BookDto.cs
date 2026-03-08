using Application.DTOs.Author;
using Application.DTOs.Category;

namespace Application.DTOs.Book;

public record BookDto(
    Guid Id,
    string Title,
    string Description,
    string? CoverUrl,
    List<AuthorPreviewDto> Authors,
    List<CategoryPreviewDto> Categories
);