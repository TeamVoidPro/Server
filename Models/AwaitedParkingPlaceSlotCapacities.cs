using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class AwaitedParkingPlaceSlotCapacities
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    [Required (ErrorMessage = "Parking place id is required")]
    public required string AwaitedParkingParkingPlaceId { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required (ErrorMessage = "Slot category id is required")]
    public required string AwaitedParkingSlotCategoryId { get; set; }
    
    public AwaitedParkingSlotCategories SlotCategories { get; set; } = null!;
    
    [Required (ErrorMessage = "Slot capacity is required")]
    [RegularExpression( @"^[0-9]+$", ErrorMessage = "Slot capacity must be a number")]
    public int AwaitedParkingSlotCapacity { get; set; }

}