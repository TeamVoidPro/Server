using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class LogoutDto
{
    [Required]
    public required string EmployeeId { get; set; }
}