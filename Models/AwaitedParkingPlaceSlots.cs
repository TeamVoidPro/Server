using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class AwaitedParkingPlaceSlots
{
    [Key]
    [Column (TypeName = "varChar(20)")]
    public required string AwaitedParkingPlaceSlotId { get; set; }
    
    public required string AwaitedParkingSlotCategoryId { get; set; }
    
    public ICollection<AwaitedParkingSlotCategories> AwaitedParkingPlaceSlotCategories { get; set; } = null!;
}