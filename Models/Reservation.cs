using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Reservation
{
    [Key]
    [Required(ErrorMessage = "Reservation id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationId { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string ZoneId { get; set; }
    
    public Zones Zone { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public required string SlotId { get; set; }
    
    public Slot Slot { get; set; } = null!;
    
    [Column(TypeName = "decimal(8,2)")]
    public required decimal TotalPayment { get; set; } 
    
    public required TimeOnly ReservationStartAt { get; set; }
    
    public required TimeOnly ReservationEndAt { get; set; }
    
    public required DateTime ReservedAt { get; set; }
    
    public required DateOnly ReservationDate { get; set; }
    
    public required DateTime ParkedAt { get; set; }
    
    public required DateTime ExitedAt { get; set; }
    
    [Column(TypeName = "varchar(10)")]
    public string PaymentMethod { get; set; } = null!;
    
    [Column(TypeName = "varchar(10)")]
    public required string PaymentStatus { get; set; }
    
    [Column(TypeName = "varchar(10)")]
    public required string ReservationStatus { get; set; }
    
    [Column(TypeName = "varchar(10)")]
    public required string ReservationType { get; set; }
    
    public required DateTime CancelledAt { get; set; }

    public required string CancellationReason { get; set; }
    
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    public BookingReservation BookingReservation { get; set; } = null!;
    
    public OnlineReservations OnlineReservations { get; set; } = null!;
    
    public OnsiteReservations OnsiteReservations { get; set; } = null!;
}