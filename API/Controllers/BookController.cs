using Application.CQRS.Book.Commands;
using Application.CQRS.Book.Queries;
using Application.DTOs.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto dto)
    {
        var result = await mediator.Send(new CreateBookCommand(dto));
        
        return CreatedAtAction(nameof(GetBook), new { id = result.Id }, result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id)
    {
        var book = await mediator.Send(new GetBookQuery(id));
        return Ok(book);
    }
}