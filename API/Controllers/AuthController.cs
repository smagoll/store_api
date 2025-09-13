using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        try
        {
            var token = await _authService.RegisterAsync(dto);

            Log.Information($"New user registered: Email={dto.Email}");

            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            Log.Error($"Error registering user: Email={dto.Email}, Error={ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        try
        {
            var token = await _authService.LoginAsync(dto);

            Log.Information($"User logged in: Email={dto.Email}");

            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            Log.Error($"Login failed for Email={dto.Email}: {ex.Message}");
            return Unauthorized(new { message = ex.Message });
        }
    }
}