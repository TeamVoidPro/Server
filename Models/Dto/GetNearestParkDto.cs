using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class GetNearestParkDto
{
    // driverLongitude , driverLatitude, radius
    [Required(ErrorMessage = "driver longitude is required.")]
    public required string DriverLongitude { get; set; }
    
    [Required(ErrorMessage = "driver latitude is required.")]
    public required string DriverLatitude { get; set; }
    
    [Required(ErrorMessage = "radius is required.")]
    public required int Radius { get; set; }
    
    // [Required(ErrorMessage = "Arrival time is required.")]
    public required string ArrivalTime { get; set; }
    
    // [Required(ErrorMessage = "Departure time is required.")]
    public required string DepartureTime { get; set; }
    
    
}