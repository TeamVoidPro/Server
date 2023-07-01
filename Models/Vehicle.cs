using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Index(
    nameof(VehicleNumber), 
    nameof(VehicleModel),
    nameof(VehicleType)
)]
public class Vehicle
{
    [Key]
    [Required (ErrorMessage = "Vehicle number is required.")]
    [RegularExpression(@"^[A-Z0-9]{3}-[0-9]{4}$", ErrorMessage = "Please enter a valid vehicle number in the format XXX-XXXX.")]
    [Column(TypeName = "varchar(8)")]
    public required string VehicleNumber { get; set; }
    
    [Required (ErrorMessage = "Vehicle type is required.")]
    [Column(TypeName = "varchar(50)")]
    public required string VehicleType { get; set; }
    
    [Required (ErrorMessage = "Vehicle model is required.")]
    [Column(TypeName = "varchar(50)")]
    public required string VehicleModel { get; set; }   
    
    public string AdditionalNotes { get; set; } = null!;
    
    public required DateTime VehicleAddedAt { get; set; } = DateTime.Now;
    
    [Required]
    public Driver Driver { get; set; } = null!;
}