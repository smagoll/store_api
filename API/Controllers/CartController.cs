using Application.DTOs;
using Application.DTOs.Cart;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CartDto>> GetCart(int userId)
    {
        var cart = await _service.GetCartAsync(userId);
        
        Log.Information($"Cart retrieved for UserId={userId} with {cart.Items.Count} items");
        
        return Ok(cart);
    }

    [HttpPost("add")]
    public async Task<ActionResult<CartDto>> AddToCart([FromBody] AddToCartDto dto)
    {
        var cart = await _service.AddToCartAsync(dto);
        
        Log.Information($"Item added to cart by UserId={dto.UserId}: ProductId={dto.ProductId}, Quantity={dto.Quantity}");
        
        return Ok(cart);
    }

    [HttpPut("update")]
    public async Task<ActionResult<CartDto>> UpdateQuantity([FromBody] UpdateCartItemDto dto)
    {
        try
        {
            var cart = await _service.UpdateQuantityAsync(dto);
            
            Log.Information($"Cart item updated for UserId={dto.UserId}: ProductId={dto.ProductId}, NewQuantity={dto.Quantity}");
            
            return Ok(cart);
        }
        catch (Exception ex)
        {
            Log.Error($"Error updating cart for UserId={dto.UserId}, ProductId={dto.ProductId}: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("remove")]
    public async Task<ActionResult<CartDto>> RemoveFromCart([FromBody] RemoveFromCartDto dto)
    {
        var cart = await _service.RemoveFromCartAsync(dto);
        
        Log.Information($"Item removed from cart by UserId={dto.UserId}: ProductId={dto.ProductId}");
        
        return Ok(cart);
    }
}