using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(order);
    }
}