using Domain.Enums;

namespace Application.DTOs.Auth;

public record UserRegisterDto(string Email, string Password);
public record UserLoginDto(string Email, string Password);
public record UserDto(int Id, string Email, UserRole Role);