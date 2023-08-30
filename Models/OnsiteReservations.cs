using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class OnsiteReservations
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public required string OnsiteReservationId { get; set; }
    
    public Reservation Reservation { get; set; } = null!;

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
    
}