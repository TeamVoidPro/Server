using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class OnsiteReservations
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationId { get; set; }

    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceOperatorId { get; set; }
    
    public Employee ParkingPlaceOperator { get; set; } = null!;
    
    [Column(TypeName = "varchar(100)")]
    public required string DriverName { get; set; }
    
    [Column(TypeName = "char(10)")]
    public required string ContactNumber { get; set; }
    
    [Column(TypeName = "varchar(10)")]
    public required string VehicleNumber { get; set; }

    [Column(TypeName = "varchar(20)")]
    public required string VehicleType { get; set; }
    
    public required DateTime ReservationStartAt { get; set; }
    
    public required DateTime ReservationEndAt { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string SlotId { get; set; }
    
    public required Slot Slot { get; set; }

    [Column(TypeName = "decimal(7,2)")]
    public required decimal TotalPayment { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string PaymentStatus { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string PaymentMethod { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string ReservationStatus { get; set; }
    
    public required DateTime ReservationCreatedAt { get; set; }
    
    public required DateTime ReservationCompletedAt { get; set; }
    
    [Column(TypeName = "varchar(1024)")]
    public required string CancellationReason { get; set; }
    
    public required DateTime CancellationCreatedAt { get; set; }
}