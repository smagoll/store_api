using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [Authorize(Policy = "RequireAdmin")]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create(CategoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        
        Log.Information($"Category created by {User.Identity?.Name}: Id={created.Id}, Name={created.Title}");
        
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        
        if (category == null)
        {
            Log.Warning($"Category with Id={id} not found");
            return NotFound();
        }
        
        Log.Information($"Category retrieved by {User.Identity?.Name}: Id={category.Id}, Name={category.Title}");
        
        return Ok(category);
    }
}