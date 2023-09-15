using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class ParkingPlaceSlotCapacitiesDto
{
    [Required(ErrorMessage = "Parking place id is required")]
    public required string ParkingPlaceId { get; set; } 
    
    [Required(ErrorMessage = "Slot category id is required")]
    public required string SlotCategoryId { get; set; }
    
    [Required(ErrorMessage = "Slot capacity is required")]
    public required int SlotCapacity { get; set; }
}