using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class VerificationCodes
{
    [Required] [Key] public required string Id { get; set; }

    [Required]
    public required string UserId { get; set; }
    
    [Required]
    public required string Code { get; set; }
    
    [Required]
    public required DateTime CreatedAt { get; set; }
    
    [Required]
    public required DateTime ExpiresAt { get; set; }
    
    [Required]
    public required bool IsUsed { get; set; }
}