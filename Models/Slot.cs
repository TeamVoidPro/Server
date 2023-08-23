using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Slot
{
    [Key]
    [Required(ErrorMessage = "Slot id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string SlotId { get; set; }
    
    [Required(ErrorMessage = "Slot number is required")]
    [RegularExpression( @"^[0-9]+$", ErrorMessage = "Slot number must be a number")]
    public required int SlotNumber { get; set; }
    
    [Required(ErrorMessage = "Slot category id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string SlotCategoryId { get; set; }
    
    public SlotCategories SlotCategories { get; set; } = null!;
    
    [Required(ErrorMessage = "Zone id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ZoneId { get; set; }
    
    public Zones Zones { get; set; } = null!;
    
    [Required(ErrorMessage = "Parking place id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceId { get; set; } = null!;
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Required(ErrorMessage = "Is available is required")]
    [RegularExpression( @"^[0-1]+$", ErrorMessage = "Is available must be a number")]
    public required bool IsAvailable { get; set; }
    
    public required string SlotStatus { get; set; }
    
    public string Description { get; set; } = null!;
    
    public DateTime SlotCreatedDate { get; set; } = DateTime.Now;
    
    public DateTime ReservedAt { get; set; }
    
    public DateTime ReservedUntil { get; set; }
    
    public ICollection<SlotReservationHistory> SlotReservationHistories { get; set; } = null!;
    
    public ICollection<Reservation> Reservations { get; set; } = null!;
    
    public ICollection<Parking> Parkings { get; set; } = null!;
    
    public ICollection<SlotRatings> SlotRatings { get; set; } = null!;
    
    public ICollection<OnsiteReservations> OnsiteReservations { get; set; } = null!;
}