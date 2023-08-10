using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class CreatePasswordDto
{
    [Required]
    public required string EmployeeId { get; set; }
    
    [Required]
    public required string Password { get; set; }
    
    [Required]
    public required string ConfirmPassword { get; set; }
}