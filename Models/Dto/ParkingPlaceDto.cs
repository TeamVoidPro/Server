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
    
    public string? ParkingPlaceVerifierId { get; set; }
    
    public string? ParkingPlaceOperatorId { get; set; }
}