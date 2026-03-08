using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public UserRole Role { get; set; } = UserRole.User;
    
    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}