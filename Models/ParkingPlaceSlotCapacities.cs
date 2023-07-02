using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ParkingPlaceSlotCapacities
{
    [Column(TypeName = "varchar(20)")]
    [Required (ErrorMessage = "Parking place id is required")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    [Required (ErrorMessage = "Slot category id is required")]
    public required string SlotCategoryId { get; set; }
    
    public SlotCategories SlotCategories { get; set; } = null!;
    
    [Required (ErrorMessage = "Slot capacity is required")]
    [RegularExpression( @"^[0-9]+$", ErrorMessage = "Slot capacity must be a number")]
    public int SlotCapacity { get; set; }
}