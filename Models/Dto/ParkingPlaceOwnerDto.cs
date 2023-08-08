using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    
    [Required (ErrorMessage = "Full is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The full name must be 2 characters long.")]
    public required string FullName { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(256, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "The password must contain at least an uppercase letter, a lowercase letter, a number and a special character.")]
    public required string Password { get; set; }
    
    [Required (ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }
    
    [Required (ErrorMessage = "Address line 1 is required")]
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 1 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public required string AddressLine1 { get; set; }
    
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 2 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public string? AddressLine2 { get; set; }
    
    [Required (ErrorMessage = "Street is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string Street { get; set; }
    
    [Required (ErrorMessage = "Street is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string City { get; set; }
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required string ContactNumber { get; set; }
    
    [Required (ErrorMessage = "Deed copy is required")]
    public required string DeedCopy { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    public required string Nic { get; set; }
    
    public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
    
    public string? Token { get; set; } = null!;
}