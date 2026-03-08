using Application.DTOs.Book;

namespace Application.DTOs.Category;

public record CategoryDto(Guid Id, string Name, List<BookPreviewDto> Books);