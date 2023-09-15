using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    public required string OwnerId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }

    public required string Email { get; set; }
    
    public required string Password { get; set; }
    
    [Required (ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }
    
    public required string AddressLine1 { get; set; }
    
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required string Province{ get; set; }
    
    public required string LandAddressNumber { get; set; }
    
    public required string LandAddressStreet { get; set; }
    
    public required string LandAddressCity { get; set; }
    
    public required string LandAddressProvince { get; set; }
    
    public required string ContactNumber { get; set; }
    
    public required string DeedCopy { get; set; }
    
    public required string LandMap { get; set; }
    
    public required string LandImages { get; set; }
    
    public required string Nic { get; set; }
    
    public required string NicFront { get; set; }
    
    public required string NicBack { get; set; }

    public DateTime AccountCreatedAt { get; set; } 
    
    public string? Token { get; set; } = null!;
}