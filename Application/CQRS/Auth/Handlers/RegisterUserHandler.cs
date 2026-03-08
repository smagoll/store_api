using Application.CQRS.Auth.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.CQRS.Auth.Handlers;

public class RegisterUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService)
    : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.dto;

        var existingUser = await userRepository.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new Exception("Пользователь с таким email уже существует");
        
        var passwordHash = passwordHasher.HashPassword(dto.Password);
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = passwordHash
        };
        
        await userRepository.AddAsync(user);
        return jwtService.GenerateToken(user);
    }
}