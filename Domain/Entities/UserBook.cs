using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class UserBook
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public ReadingStatus Status { get; set; }
    public int? Rating { get; set; }
    public DateTime AddedAt { get; set; }
}