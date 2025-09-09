using Application.DTOs;
using Application.DTOs.Cart;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartDto>> AddToCart([FromBody] AddToCartDto dto)
        {
            var cart = await _service.AddToCartAsync(dto);
            return Ok(cart);
        }

        [HttpPut("update")]
        public async Task<ActionResult<CartDto>> UpdateQuantity([FromBody] UpdateCartItemDto dto)
        {
            try
            {
                var cart = await _service.UpdateQuantityAsync(dto);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("remove")]
        public async Task<ActionResult<CartDto>> RemoveFromCart([FromBody] RemoveFromCartDto dto)
        {
            var cart = await _service.RemoveFromCartAsync(dto);
            return Ok(cart);
        }

        [HttpPost("checkout/{userId}")]
        public async Task<ActionResult<OrderDto>> Checkout(int userId)
        {
            return StatusCode(501, "Checkout not implemented yet");
        }
    }
}