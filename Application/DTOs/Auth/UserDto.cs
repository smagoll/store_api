using Domain.Enums;

namespace Application.DTOs.Auth;

public record UserDto(int Id, string Email, UserRole Role);