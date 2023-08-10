using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Server.Models;

public class Employee
{
    [Key]
    [Required (ErrorMessage = "Employee id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string EmployeeId { get; set; }
    
    [Required (ErrorMessage = "First name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid first name")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "The first name must be 2 characters long.")]
    public required string FirstName { get; set; }
    
    [Required (ErrorMessage = "Last name is required")]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid last name")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "The last name must be 2 characters long.")]
    public required string LastName { get; set; }
    
    [Required (ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [Column(TypeName = "varchar(100)")]
    public required string Email { get; set; }
    
    [StringLength(256, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "The password must contain at least an uppercase letter, a lowercase letter, a number and a special character.")]
    [AllowNull]
    public string Password { get; set; }
    
    [Required (ErrorMessage = "Address line 1 is required")]
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 1 should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The address line 1 must be 2 characters long.")]
    public required string AddressLine1 { get; set; }
    
    [RegularExpression(@"^[a-zA-Z0-9/\s]+$", ErrorMessage = "Address line 1 should contain only alphanumeric characters.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "The address line 1 must be 2 characters long.")]
    public string? AddressLine2 { get; set; }
    
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
    public required string ContactNumber { get; set; }
    
    [Required (ErrorMessage = "NIC is required")]
    [Column(TypeName = "varchar(12)")]
    [MaxLength(12, ErrorMessage = "NIC must be 12 digits")]
    [MinLength(11, ErrorMessage = "NIC must be at least 11 digits")]
    [RegularExpression(@"^[0-9Vv]+$" , ErrorMessage = "Invalid NIC")]
    public required string Nic { get; set; }
    
    [Required]
    [Column(TypeName = "varchar(20)")]
    public required string Role { get; set; }
    
    [Column(TypeName = "varchar(256)")]
    [AllowNull]
    public string ProfilePicture { get; set; } = null!;
    
    [Required]
    public DateTime AccountCreatedAt { get; set; }
    
    [Column(TypeName = "varchar(512)")]
    [AllowNull]
    public string Token { get; set; }

    [AllowNull]
    public bool IsActivated { get; set; } 
    
    public bool IsVerified { get; set; }
    
    [NotMapped]
    public ParkingPlace VerifiedParkingPlace { get; set; } = null!;
    
    [NotMapped]
    public ParkingPlace OperatorParkingPlace { get; set; } = null!;
    
    public ICollection<Reservation> Reservation { get; set; } = null!;
    
    public ICollection<Ticket> Ticket { get; set; } = null!;
    
    public ICollection<AwaitedParkingPlaces> AwaitedParkingPlaces { get; set; } = null!;
    
    public ICollection<ComplianceMonitoring> ComplianceMonitoring { get; set; } = null!;
    
    public ICollection<Issues> Issues { get; set; } = null!;
    
    public ICollection<RefreshToken> RefreshToken { get; set; } = null!;
 }