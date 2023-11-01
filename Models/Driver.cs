using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;


public class Driver
{
    [Key]
    [Required]
    [Column(TypeName = "varchar(20)")]
    public required string DriverId { get; set; }
    
    [Required (ErrorMessage = "First name is required")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "The first name must be 2 characters long.")]
    public required string FirstName { get; set; }
    
    [Required (ErrorMessage = "Last name is required")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "The last name must be 2 characters long.")]
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
    
    [Required (ErrorMessage = "Contact number is required")]
    [MaxLength(10, ErrorMessage = "Contact number must be 10 digits")]
    [MinLength(10, ErrorMessage = "Contact number must be 10 digits")]
    public required string ContactNumber { get; set; }
    
    // [Column(TypeName = "varchar(256)")]
    // [AllowNull]
    // public string ProfilePicture { get; set; } = null!;
    
    [Required]
    public DateTime AccountCreatedAt { get; set; }
    
    public string Token { get; set; } = null!;
    public ICollection<Vehicle>? Vehicles { get; set; } 
    
    public ICollection<ParkingPlaceRatings> ParkingPlaceRatings { get; set; } = null!;
    
    public ICollection<Issues> Issues { get; set; } = null!; 
    
    public ICollection<SlotRatings> SlotRatings { get; set; } = null!;
    
    public ICollection<DriverRefreshToken> RefreshTokens { get; set; } = null!;
}