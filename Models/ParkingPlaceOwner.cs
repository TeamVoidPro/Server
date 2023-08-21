using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Server.Models;

public class ParkingPlaceOwner
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public required string OwnerId { get; set; }
    
    [Required (ErrorMessage = "First Name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name name must be 2 characters long.")]
    public required string FirstName { get; set; }
    
    [Required (ErrorMessage = "Last Name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name name must be 2 characters long.")]
    public required string LastName { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Column(TypeName = "varchar(100)")]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "Password is required")]
    [StringLength(256, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "The password must contain at least an uppercase letter, a lowercase letter, a number and a special character.")]
    public required string Password { get; set; }

    [Required (ErrorMessage = "Address line 1 is required")]
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 1 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    public required string AddressLine1 { get; set; }
    
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 2 should contain only alphanumeric characters.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Invalid address line.")]
    [AllowNull]
    public string? AddressLine2 { get; set; }
    
    [Required (ErrorMessage = "City is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "City should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid City.")]
    public required string City { get; set; }
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required string ContactNumber { get; set; }
    
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    public required string NIC { get; set; }
    
    [Required (ErrorMessage = "NIC Front Image is required")]
    [Column(TypeName = "varchar(12)")]
    public required string NICFront { get; set; }
    
    [Required (ErrorMessage = "NIC Back Image is required")]
    [Column(TypeName = "varchar(12)")]
    public required string NICBack { get; set; }
    
    [Required (ErrorMessage = "Account Created At is required")]
    public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
    
    [Required (ErrorMessage = "Account Verified is required")]
    public bool AccountVerified { get; set; } = false;
    
    [Required (ErrorMessage = "Is Registered is required")]
    public bool IsRegistered { get; set; } = false;
    
    public int CurrentRegistrationStep { get; set; } = 0;
    
    [Required (ErrorMessage = "Is Blocked is required")]
    public bool IsBlocked { get; set; } = false;
    
    [Required (ErrorMessage = "Is Deleted is required")]
    public bool IsDeleted { get; set; } = false;
    
    
    [Column(TypeName = "varchar(512)")]
    [AllowNull]
    public string Token { get; set; } = null!;
    
    public ICollection<ParkingPlace>? ParkingPlaces { get; set; }
    
    public ICollection<AwaitedParkingPlaces>? AwaitedParkingPlaces { get; set; }
}