using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ZonePlan
{
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Zone plan id is required")]
    public required string ZonePlanId { get; set; }

    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Booking plan id is required")]
    public required string BookingPlanId { get; set; }

    public BookingPlan BookingPlan { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Zone id is required")]
    public required string ZoneId { get; set; }
    
    public Zones Zone { get; set; } = null!;

    [Column(TypeName = "decimal(8,2)")]
    [Required(ErrorMessage = "Price is required")]
    public required decimal Price { get; set; }
    
    public ICollection<BookingReservation> BookingReservations { get; set; } = null!;
    
    public Vehicle Vehicle { get; set; } = null!;
    
}