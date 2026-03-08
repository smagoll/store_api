using Application.CQRS.Book.Commands;
using Application.DTOs.Book;
using AutoMapper;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.CQRS.Book.Handlers;

public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper) 
    : IRequestHandler<CreateBookCommand, BookDto>
{
    public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var dto = request.dto;

        var book = new Domain.Entities.Book
        {
            Title = dto.Title,
            Description = dto.Description,
            CoverUrl = dto.CoverUrl
        };

        
        var authors = await authorRepository.GetAllByIdsAsync(dto.AuthorIds);

        book.Authors = authors;

        if (dto.CategoryIds != null && dto.CategoryIds.Any())
        {
            var categories = await categoryRepository.GetAllByIdsAsync(dto.CategoryIds);

            book.Categories = categories;
        }
        
        await bookRepository.AddAsync(book);
        
        return mapper.Map<BookDto>(book);
    }
}