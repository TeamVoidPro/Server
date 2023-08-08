using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class RefreshTokenDto
{
    [Required]
    public required string Token { get; set; }
    
    [Required]
    public required string RefreshToken { get; set; }
}