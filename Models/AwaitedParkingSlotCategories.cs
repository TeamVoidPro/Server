using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class AwaitedParkingSlotCategories
{
    [Key]
    [Required(ErrorMessage = "Awaited Slot category id is required")]
    [Column (TypeName = "varChar(20)")]
    public required string AwaitedParkingSlotCategoryId { get; set; }
    
    [Required(ErrorMessage = "Slot category name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The slot category name must be 2 characters long.")]
    public required string AwaitedParkingPlaceSlotCategoryName { get; set; }
    
    public string? AwaitedParkingSlotCategoryDescription { get; set; } = null!;
    
    public ICollection<AwaitedParkingPlaceSlotCapacities> ParkingPlaceSlotCapacities { get; set; } = null!;
    
    public ICollection<AwaitedParkingPlaceSlots> AwaitedParkingPlaceSlots { get; set; } = null!;

    
}