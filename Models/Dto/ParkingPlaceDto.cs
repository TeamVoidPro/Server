namespace Server.Models.Dto;

public class ParkingPlaceDto
{
    public required string ParkingPlaceId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Location { get; set; }
    
    public string Description { get; set; } = null!;
    
    public required string ParkingPlaceOwnerId { get; set; }
    
    public string? ParkingPlaceVerifierId { get; set; }
    
    public string? ParkingPlaceOperatorId { get; set; }
}