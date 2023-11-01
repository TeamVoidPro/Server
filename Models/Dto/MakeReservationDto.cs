using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class MakeReservationDto
{
    [Required(ErrorMessage = "Zone id is required")]
    public required string ZoneId { get; set; }
    
    [Required(ErrorMessage = "Reservation start time is required")]
    public required DateTime ReservationStartTime { get; set; }
    
    [Required(ErrorMessage = "Reservation end time is required")]
    public required DateTime ReservationEndTime { get; set; }
    
    [Required(ErrorMessage = "ParkingPlaceId is required")]
    public required string ParkingPlaceId { get; set; }
    
    [Required(ErrorMessage = "Total payment is required")]
    public required decimal TotalPayment { get; set; }
    
    //PaymentMethod
    [Required(ErrorMessage = "Payment method is required")]
    public required string PaymentMethod { get; set; } = "Card";

}