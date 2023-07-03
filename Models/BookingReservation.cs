using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class BookingReservation
{
    [Key]
    [Required(ErrorMessage = "Booking reservation id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string BookingReservationId { get; set; }
    
    [Required(ErrorMessage = "Zone plan id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ZonePlanId { get; set; }
    
    public required string BookingPlanId { get; set; }
    public ZonePlan ZonePlan { get; set; } = null!;

    [Required(ErrorMessage = "Reservation date is required")]
    public DateTime ReservationDate { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "Reservation start time is required")]
    public DateTime ReservationStartTime { get; set; }
    
    [Required(ErrorMessage = "Reservation end time is required")]
    public DateTime ReservationEndTime { get; set; }
    
    [Required(ErrorMessage = "Total payment is required")]
    [Column(TypeName = "decimal(8,2)")]
    public required decimal TotalPayment { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; } 

    [Required(ErrorMessage = "Vehicle number is required")]
    [Column(TypeName = "varchar(8)")]
    public string VehicleNumber { get; set; } = null!;

    public Vehicle Vehicle { get; set; } = null!;
    
    public ICollection<Parking> Parkings { get; set; } = null!;
}