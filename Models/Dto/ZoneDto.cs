using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ZoneDto
{
    [Required(ErrorMessage = "Zone name is required")]
    public required string ZoneName { get; set; }
    
    [Required(ErrorMessage = "Zone price is required")]
    public required decimal ZonePrice { get; set; }
    
    public string? ZoneDescription { get; set; }
    
    [Required(ErrorMessage = "Parking place id is required")]
    public string ParkingPlaceId { get; set; } = null!;
    
    public required DateTime ZoneCreatedDate { get; set; }

}