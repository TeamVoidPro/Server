using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    
    [Required (ErrorMessage = "Full is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The full name must be 2 characters long.")]
    public required string FullName { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(256, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
    //     ErrorMessage = "The password must contain at least an uppercase letter, a lowercase letter, a number and a special character.")]
    public required string Password { get; set; }
    

    
    [Required (ErrorMessage = "Address line 1 is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public required string AddressLine1 { get; set; }
    
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public string? AddressLine2 { get; set; }
    
    [Required (ErrorMessage = "Street is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string City { get; set; }
    
    [Required (ErrorMessage = "Street is required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string Province { get; set; }
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required string ContactNumber { get; set; }

    [Required (ErrorMessage = "NIC is required")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    public required string Nic { get; set; }
    
    public required string IdentificationMethod { get; set; }
    
    public required string IdentificationFrontImage { get; set; }
    
    public required string IdentificationBackImage { get; set; }
    
    public required string LandAddress1 { get; set; }

    public string? LandStreet { get; set; }
    
    public required string LandCity { get; set; }
    
    public required string LandProvince { get; set; }
    
    public required string LandDeedImage { get; set; }
    
    public required string LandMapImage { get; set; }
    
    public required string LandImages { get; set; }
    
    public required double Longitude { get; set; }
    
    public required double Latitude { get; set; }
    
    
    
}