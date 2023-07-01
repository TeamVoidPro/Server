namespace Server.Models.Dto;

public class VehicleDto
{
    public required string VehicleNumber { get; set; }
    
    public required string VehicleType { get; set; }
    
    public required string VehicleModel { get; set; }
    
    public string AdditionalNotes { get; set; } = null!;
    
    public required DateTime VehicleAddedAt { get; set; } = DateTime.Now;
}