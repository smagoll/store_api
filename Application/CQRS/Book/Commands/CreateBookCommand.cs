using Application.DTOs.Book;
using MediatR;

namespace Application.CQRS.Book.Commands;

public record CreateBookCommand(CreateBookDto dto) : IRequest<BookDto>;