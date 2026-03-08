using Application.CQRS.Auth.Commands;
using Application.CQRS.Auth.Handlers;
using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        try
        {
            var token = await mediator.Send(new RegisterUserCommand(dto));

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
            var token = await mediator.Send(new LoginUserCommand(dto));

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