using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class SlotCategories
{
    [Key]
    [Required(ErrorMessage = "Slot category id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string SlotCategoryId { get; set; }
    
    [Required(ErrorMessage = "Slot category name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The slot category name must be 2 characters long.")]
    public required string SlotCategoryName { get; set; }
    
    public string? SlotCategoryDescription { get; set; } = null!;
    
    public DateTime CategoryCreatedDate { get; set; } = DateTime.Now;
    
    public ICollection<ParkingPlaceSlotCapacities> ParkingPlaceSlotCapacities { get; set; } = null!;
    
    public ICollection<Slot> Slots { get; set; } = null!;
}