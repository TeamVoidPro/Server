using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class EmployeeIdDto
{
    [Required]
    public required string EmployeeId { get; set; }
}