using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ParkingPlaceOwner
{
    [Key]
    [Required (ErrorMessage = "Parking place owner id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string OwnerId { get; set; }
    
    [Required (ErrorMessage = "Full is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The full name must be 2 characters long.")]
    public required string FullName { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Column(TypeName = "varchar(100)")]
    public required string Email { get; set; }
    
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
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required int ContactNumber { get; set; }
    
    [Required (ErrorMessage = "Deed copy is required")]
    [Column(TypeName = "varchar(256)")]
    public required string DeedCopy { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    public required string Nic { get; set; }
}