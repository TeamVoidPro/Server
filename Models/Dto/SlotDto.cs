using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class SlotDto
{
    [Required(ErrorMessage = "Slot number is required")]
    public required int SlotNumber { get; set; }
    
    [Required(ErrorMessage = "Slot category id is required")]
    public required string SlotCategoryId { get; set; }
    
    [Required(ErrorMessage = "Zone id is required")]
    public required string ZoneId { get; set; }
    
    [Required(ErrorMessage = "Parking place id is required")]
    public required string ParkingPlaceId { get; set; }
    
    [Required(ErrorMessage = "Is available is required")]
    public required bool IsAvailable { get; set; }
    
    [Required(ErrorMessage = "Slot status is required")]
    public required string SlotStatus { get; set; }
    
    public string Description { get; set; } = null!;
    
    public DateTime SlotCreatedDate { get; set; } 
    
    public TimeOnly ReservedAt { get; set; }
    
    public TimeOnly ReservedUntil { get; set; }
}