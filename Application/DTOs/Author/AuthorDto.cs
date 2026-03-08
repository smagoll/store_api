using Application.DTOs.Book;

namespace Application.DTOs.Author;

public record AuthorDto(Guid Id, string Name, string? Biography, List<BookPreviewDto> Books);