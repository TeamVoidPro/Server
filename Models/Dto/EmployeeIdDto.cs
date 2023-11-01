using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class EmployeeIdDto
{
    
    public string? EmployeeId { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Nic { get; set; }

    public string? ContactNumber { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }
    
}