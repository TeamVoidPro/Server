using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class BookingReservation
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public required string BookingReservationId { get; set; }
    
    public Reservation Reservation { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public required string ZonePlanId { get; set; }
    
    public required string BookingPlanId { get; set; }
    public ZonePlan ZonePlan { get; set; } = null!;

    public DateTime ParkedAt { get; set; }
    
    public DateTime ExitedAt { get; set; }

    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; } 

    [Column(TypeName = "varchar(8)")]
    public string VehicleNumber { get; set; } = null!;

    public Vehicle Vehicle { get; set; } = null!;
    
    public ICollection<Parking> Parkings { get; set; } = null!;
}