using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class GetNearestParkDto
{
    [Required(ErrorMessage = "driver longitude is required.")]
    public required double DriverLongitude { get; set; }
    
    [Required(ErrorMessage = "driver latitude is required.")]
    public required double DriverLatitude { get; set; }
    
    [Required(ErrorMessage = "radius is required.")]
    public required double Radius { get; set; }
    
    // [Required(ErrorMessage = "Arrival time is required.")]
    public required DateTime ArrivalTime { get; set; }
    
    // [Required(ErrorMessage = "Departure time is required.")]
    public required DateTime DepartureTime { get; set; }
    
    
}