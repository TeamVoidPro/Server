using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class VerifyAccountDto
{
    [Required]
    public required string EmployeeId { get; set; } 
    
    [Required]
    public required string VerificationCode { get; set; }
}