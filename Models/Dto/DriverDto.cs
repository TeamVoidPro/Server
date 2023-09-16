using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;


public class DriverDto
{
    
    public required string FirstName { get; set; }
    
    
    public required string LastName { get; set; } 
    

    public required string Email { get; set; } 
    
    public required string Password { get; set; } 
    
    [Required (ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }
    
    public required string ContactNumber { get; set; }
    
}