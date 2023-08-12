namespace Server.Models.Dto;

public class VehicleDto
{
    public required string DriverId { get; set; } = "Drv_7228_4374";
    public required string VehicleNumber { get; set; }
    
    public required string VehicleType { get; set; }
    
    public required string VehicleColor { get; set; }
    public required string VehicleModel { get; set; }
    
    public string AdditionalNotes { get; set; } = null!;
    
}