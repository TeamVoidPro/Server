using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class EmployeeDto
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    public required string AddressLine1 { get; set; }
    
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required string ContactNumber { get; set; }
    
    public required string Nic { get; set; }
    
    public required string Role { get; set; }
    
    public DateTime AccountCreatedAt { get; set; }
}