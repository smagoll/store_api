using Application.DTOs.Book;

namespace Application.DTOs.Category;

public record CategoryPreviewDto(Guid Id, string Name, List<BookDto> Books);