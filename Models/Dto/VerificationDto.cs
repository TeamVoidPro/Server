using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class VerificationDto
{
    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string VerificationCode { get; set; }
}