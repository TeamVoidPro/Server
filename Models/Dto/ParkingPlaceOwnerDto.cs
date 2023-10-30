using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{

    public required string OwnerId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The first name must be 1 characters long.")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Second name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The last name must be 1 characters long.")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The email must be 1 characters long.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(256, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "The password must contain at least an uppercase letter, a lowercase letter, a number and a special character.")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Confirm password is required")]
    public required string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Address line 1 is required")]
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 1 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public required string AddressLine1 { get; set; }

    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 2 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public string? AddressLine2 { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string Street { get; set; }

    [Required(ErrorMessage = "Street is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string City { get; set; }

    public required string ContactNumber { get; set; }

    public required string DeedCopy { get; set; }

    public required string Nic { get; set; }

    public DateTime AccountCreatedAt { get; set; }

    public string? Token { get; set; } = null!;
    public required string Province { get; set; }
    public required string LandAddressNumber { get; set; }
    public required string LandAddressStreet { get; set; }
    public required string LandAddressCity { get; set; }
    public required string LandAddressProvince { get; set; }
    public required string LandMap { get; set; }
    public required string LandImages { get; set; }
    public required string NicFront { get; set; }
    public required string NicBack { get; set; }
    public bool acceptStatus { get; set; }
}