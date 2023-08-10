using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class EmployeeDto
{
    
    [Required(ErrorMessage = "Firstname is required")]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "Lastname is required")]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "AddressLine1 is required")]
    public required string AddressLine1 { get; set; }
    
    public string? AddressLine2 { get; set; }
    
    [Required(ErrorMessage = "Street is required")]
    public required string Street { get; set; }
    
    [Required(ErrorMessage = "City is required")]
    public required string City { get; set; }
    
    [Required(ErrorMessage = "ContactNumber is required")]
    public required string ContactNumber { get; set; }
    
    [Required(ErrorMessage = "Nic is required")]
    public required string Nic { get; set; }
    
    [Required(ErrorMessage = "Role is required")]
    public required string Role { get; set; }
    
    public DateTime AccountCreatedAt { get; set; }
}