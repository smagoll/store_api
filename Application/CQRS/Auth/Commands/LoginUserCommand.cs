using Application.DTOs.Auth;
using MediatR;

namespace Application.CQRS.Auth.Commands;

public record LoginUserCommand(UserLoginDto dto) : IRequest<string>;