using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }
    
    [HttpPost("checkout/{userId}")]
    public async Task<ActionResult<OrderDto>> Checkout(int userId)
    {
        var order = await _service.CheckoutAsync(userId);
        
        Log.Information($"Order Id({order.Id}) checked for {userId}");
        
        return Ok(order);
    }
}