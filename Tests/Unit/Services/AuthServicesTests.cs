using Application.DTOs.Auth;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace Tests.Unit.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<AutoMapper.IMapper> _mapperMock;

    private readonly AuthService _service;

    public AuthServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _jwtServiceMock = new Mock<IJwtService>();
        _mapperMock = new Mock<AutoMapper.IMapper>();

        _service = new AuthService(
            _userRepoMock.Object,
            _passwordHasherMock.Object,
            _jwtServiceMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrow_WhenEmailAlreadyExists()
    {
        // Arrange
        var dto = new UserRegisterDto("test@example.com", "password");
        _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email))
                     .ReturnsAsync(new User { Email = dto.Email });

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.RegisterAsync(dto));
    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUserAndReturnToken()
    {
        // Arrange
        var dto = new UserRegisterDto("new@example.com", "password");
        var user = new User { Email = dto.Email };
        var token = "fake-jwt-token";

        _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);
        _passwordHasherMock.Setup(h => h.HashPassword(dto.Password)).Returns("hashed-password");
        _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
        _jwtServiceMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns(token);

        // Act
        var result = await _service.RegisterAsync(dto);

        // Assert
        Assert.Equal(token, result);
        _userRepoMock.Verify(r => r.AddAsync(It.Is<User>(u => u.Email == dto.Email && u.PasswordHash == "hashed-password")), Times.Once);
        _jwtServiceMock.Verify(j => j.GenerateToken(It.Is<User>(u => u.Email == dto.Email)), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrow_WhenUserNotFound()
    {
        // Arrange
        var dto = new UserLoginDto("notfound@example.com", "password");
        _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.LoginAsync(dto));
    }

    [Fact]
    public async Task LoginAsync_ShouldThrow_WhenPasswordInvalid()
    {
        // Arrange
        var dto = new UserLoginDto("test@example.com", "password");
        var user = new User { Email = dto.Email, PasswordHash = "hashed-password" };

        _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.VerifyPassword(dto.Password, user.PasswordHash)).Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.LoginAsync(dto));
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsValid()
    {
        // Arrange
        var dto = new UserLoginDto("test@example.com", "password");
        var user = new User { Email = dto.Email, PasswordHash = "hashed-password" };
        var token = "jwt-token";

        _userRepoMock.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.VerifyPassword(dto.Password, user.PasswordHash)).Returns(true);
        _jwtServiceMock.Setup(j => j.GenerateToken(user)).Returns(token);

        // Act
        var result = await _service.LoginAsync(dto);

        // Assert
        Assert.Equal(token, result);
        _jwtServiceMock.Verify(j => j.GenerateToken(user), Times.Once);
    }
}
