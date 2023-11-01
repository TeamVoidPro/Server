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
    public required string Radius { get; set; }
    
}