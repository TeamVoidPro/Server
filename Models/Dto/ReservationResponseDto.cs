namespace Server.Models.Dto;

public class ReservationResponseDto
{
    public string? Name { get; set; }
    
    public string? ReservationID { get; set; }
    
    public TimeOnly? ReservationStartedAt { get; set; }
    
    public TimeOnly? ReservationEndedAt { get; set; }
    
    public string? ContactNumber { get; set; }
    
    public string? VehicleNumber { get; set; }
    
    public string? VehicleType { get; set; }
    
    public string? VehicleModel { get; set; }
    
    public int? SlotNumber { get; set; }
}