using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    public required string OwnerId { get; set; }
    
    public required string FullName { get; set; }
    
    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    [Required (ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }
    
    public required string AddressLine1 { get; set; }
    
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required int ContactNumber { get; set; }
    
    public required string DeedCopy { get; set; }
    
    public required string Nic { get; set; }
    
    public DateTime AccountCreatedAt { get; set; } 
    
    public string? Token { get; set; } = null!;
}