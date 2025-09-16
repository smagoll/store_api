using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Xunit;

public class JwtServiceTests
{
    private readonly JwtSettings _settings;
    private readonly JwtService _service;

    public JwtServiceTests()
    {
        _settings = new JwtSettings
        {
            Key = "super_secret_test_key_12345123123123",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            ExpirationMinutes = 30
        };

        var options = Options.Create(_settings);
        _service = new JwtService(options);
    }

    [Fact]
    public void GenerateToken_ShouldReturnValidJwt()
    {
        // Arrange
        int id = 1;
        string email = "test@example.com";
        UserRole role = UserRole.Admin;
        
        var user = new User
        {
            Id = id,
            Email = email,
            Role = role
        };

        // Act
        var token = _service.GenerateToken(user);

        // Assert
        Assert.False(string.IsNullOrEmpty(token));

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        Assert.Equal(_settings.Issuer, jwt.Issuer);
        Assert.Contains(_settings.Audience, jwt.Audiences);

        Assert.Equal(id.ToString(), jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
        Assert.Equal(email, jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value);
        Assert.Equal(role.ToString(), jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);

        var expectedExpiration = DateTime.UtcNow.AddMinutes(_settings.ExpirationMinutes);
        Assert.True(jwt.ValidTo > DateTime.UtcNow);
        Assert.InRange(jwt.ValidTo, expectedExpiration.AddSeconds(-5), expectedExpiration.AddSeconds(5));
    }
}