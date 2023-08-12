using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Reservation
{
    [Key]
    [Required(ErrorMessage = "Reservation id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationId { get; set; }
    
    [Required(ErrorMessage = "Zone id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ZoneId { get; set; }
    
    public Zones Zones { get; set; } = null!;
    
    [Required(ErrorMessage = "Total amount is required")]
    [RegularExpression(@"^\d{1,4}(\.\d{1,2})?$", ErrorMessage = "Please enter a valid total amount.")]
    [Column(TypeName = "decimal(8,2)")]
    public required decimal TotalAmount { get; set; }
    
    [Required(ErrorMessage = "Payment status is required")]
    [Column(TypeName = "varchar(10)")]
    public required string PaymentStatus { get; set; }
    
    [Required(ErrorMessage = "Payment method is required")]
    [Column(TypeName = "varchar(20)")]
    public required string PaymentMethod { get; set; }
    
    [Required(ErrorMessage = "Reservation type is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationType { get; set; }
    
    [Required(ErrorMessage = "Reservation status is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationStatus { get; set; }
    
    [Required(ErrorMessage = "Reservation created date is required")]
    public DateTime ReservationCreatedDate { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "Reservation start time is required")]
    public DateTime ReservationStartTime { get; set; }
    
    [Required(ErrorMessage = "Reservation end time is required")]
    public DateTime ReservationEndTime { get; set; }
    
    [Required(ErrorMessage = "Reservation date is required")]
    public DateTime ReservationDate { get; set; }
    
    public DateTime ReservationCancelledAt { get; set; }
    
    public string? CancellationReason { get; set; }
    
    public string? SpecialNotes { get; set; }
    
    public DateTime ParkingStartedAt { get; set; }
    
    public DateTime ParkingEndedAt { get; set; }

    public string SlotId { get; set; } = null!;
    
    public Slot Slot { get; set; } = null!;
    
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    public required string VehicleNumber { get; set; }
    
    public Vehicle Vehicle { get; set; } = null!;
    public SlotReservationHistory SlotReservationHistory { get; set; } = null!;
}