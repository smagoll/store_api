using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class UserBook
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }

    public ReadingStatus Status { get; set; }
    public int? Rating { get; set; }
    public DateTime AddedAt { get; set; }
}