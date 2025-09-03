using Application.DTOs.Auth;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(UserRegisterDto dto);
    Task<string> LoginAsync(UserLoginDto dto);
}