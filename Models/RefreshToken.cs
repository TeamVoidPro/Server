using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class RefreshToken
{
    [Key] 
    [Required] 
    public required string Id { get; set; }

    [Required]
    public required string EmployeeId { get; set; }
    
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

    public Employee Employee { get; set; }  = null!;
}