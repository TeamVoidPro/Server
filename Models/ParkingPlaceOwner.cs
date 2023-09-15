using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ParkingPlaceOwner
{
    [Key]
    [Required (ErrorMessage = "Parking place owner id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string OwnerId { get; set; }
    
    [Required (ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The first name must be 1 characters long.")]
    public required string FirstName { get; set; }
    
    [Required (ErrorMessage = "Second name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The last name must be 1 characters long.")]
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
    
    [Required (ErrorMessage = "Street is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    public required string Street { get; set; }
    
    [Required (ErrorMessage = "City is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City should contain only alphabetic characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid city.")]
    public required string City { get; set; }
    
    [Required (ErrorMessage = "Province is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Province should contain only alphabetic characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid province.")]
    public required string Province { get; set; }
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    
    public required string LandAddressNumber { get; set; }
    
    [Required (ErrorMessage = "Number is required")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid street.")]
    
    public required string LandAddressStreet { get; set; }
    
    [Required (ErrorMessage = "City is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City should contain only alphabetic characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid city.")]
    public required string LandAddressCity { get; set; }
    
    [Required (ErrorMessage = "Province is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Province should contain only alphabetic characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Invalid province.")]
    public required string LandAddressProvince { get; set; }
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required string ContactNumber { get; set; }
    
    [Required (ErrorMessage = "Deed copy is required")]
    [Column(TypeName = "varchar(256)")]
    public required string DeedCopy { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    
    public required string LandMap { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    
    public required string LandImages { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    public required string Nic { get; set; }
    
    [Required (ErrorMessage = "Front view of NIC is required")]
    [Column(TypeName = "varchar(256)")]
    public required string NicFront { get; set; }
    
    [Required (ErrorMessage = "Back view of NIC is required")]
    [Column(TypeName = "varchar(256)")]
    public required string NicBack { get; set; }
    
    public DateTime AccountCreatedAt { get; set; } = DateTime.Now;
    
    [Column(TypeName = "varchar(256)")]
    public string Token { get; set; } = null!;
    
    public ICollection<ParkingPlace>? ParkingPlaces { get; set; }
    
    public ICollection<AwaitedParkingPlaces>? AwaitedParkingPlaces { get; set; }
}