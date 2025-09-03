using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;
}