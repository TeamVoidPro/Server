using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Parking
{
    
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Parking id is required")]
    public required string ParkingId { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Booking reservation id is required")]
    public required string BookingReservationId { get; set; }
    
    public BookingReservation BookingReservation { get; set; } = null!;
    
    [Required(ErrorMessage = "Parking date is required")]
    public DateTime ParkingDate { get; set; }
    
    public DateTime ParkingStartTime { get; set; }
    
    public DateTime ParkingEndTime { get; set; }
    
    public string ParkedDuration { get; set; } = null!;

    public bool IsParkOnNextDay { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public string SlotId { get; set; } = null!;
    
    public Slot Slot { get; set; } = null!;
}