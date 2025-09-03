using Application.DTOs.Auth;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _mapper = mapper;
    }
    
    public Task<string> RegisterAsync(UserRegisterDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<string> LoginAsync(UserLoginDto dto)
    {
        throw new NotImplementedException();
    }
}