using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ZoneReservation
{
    [Column (TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Zone vehicle number is required")]
    public required string VehicleNumber { get; set; } 
    
    public required Vehicle Vehicle { get; set; } = null!;
    
    [Column (TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Parking place id is required")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Column (TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Reservation id is required")]
    public required string ReservationId { get; set; }
    
    public Reservation Reservation { get; set; } = null!;
}