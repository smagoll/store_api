using Application.DTOs.Auth;
using MediatR;

namespace Application.CQRS.Auth.Commands;

public record RegisterUserCommand(UserRegisterDto dto) : IRequest<string>;