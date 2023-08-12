using System.ComponentModel.DataAnnotations;

namespace Server.Models;

public class DriverRefreshToken
{
    [Key] 
    [Required] 
    public required string Id { get; set; }

    [Required]
    public required string DriverId { get; set; }
    
    [Required]
    public required string Token { get; set; }
    
    [Required]
    public required string JwtId { get; set; }
    
    [Required]
    public required DateTime Expires { get; set; }
    
    [Required]
    public required bool IsUsed { get; set; }
    
    [Required]
    public required bool IsRevoked { get; set; }

    public Driver Driver { get; set; }  = null!;
}