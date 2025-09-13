using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [Authorize(Policy = "RequireAdmin")]
    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var created = await _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _service.GetById(id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _service.GetAll();
        return Ok(products);
    }
    
    [Authorize(Policy = "RequireAdmin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(DeleteProductDto dto)
    {
        await _service.Delete(dto);
        return NoContent();
    }
}