using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Server.Models.Dto;

public class ParkingPlaceDto
{
    [Required(ErrorMessage = "Parking place name is required.")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Parking place location is required.")]
    public required string Location { get; set; }
    
    [AllowNull]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Parking place owner id is required.")]
    public required string ParkingPlaceOwnerId { get; set; }
    
    [Required(ErrorMessage = "Parking place verifier id is required.")]
    public string? ParkingPlaceVerifierId { get; set; }
    
    [Required(ErrorMessage = "Parking place operator id is required.")]
    public string? ParkingPlaceOperatorId { get; set; }
}