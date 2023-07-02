namespace Server.Models.Dto;

public class ParkingPlaceRatingsDto
{
    public required string RatingId { get; set; }
    
    public required float Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime RatingDate { get; set; } = DateTime.Now;
    
    public required string ParkingPlaceId { get; set; }

    public required string DriverId { get; set; }
}