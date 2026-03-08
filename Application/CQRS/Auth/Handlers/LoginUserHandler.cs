using Application.CQRS.Auth.Commands;
using Application.Interfaces;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.CQRS.Auth.Handlers;

public class LoginUserHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService)
    : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.dto;
        
        var user = await userRepository.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new Exception("Неверный email или пароль");

        var isPasswordValid = passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);
        if (!isPasswordValid)
            throw new Exception("Неверный email или пароль");

        var token = jwtService.GenerateToken(user);

        return token;
    }
}