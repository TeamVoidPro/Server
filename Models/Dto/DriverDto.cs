using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;


public class DriverDto
{
    public required string DriverId { get; set; }
    
    public required string FirstName { get; set; }
    
    
    public required string LastName { get; set; } 
    

    public required string Email { get; set; } 
    
    public required string Password { get; set; } 
    
    [Required (ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }
    
    public required int ContactNumber { get; set; }

    public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
}