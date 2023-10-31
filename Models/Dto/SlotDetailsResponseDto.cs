namespace Server.Models.Dto;

public class SlotDetailsResponseDto
{
    public string? SlotId { get; set; }
    
    public string? ZoneName { get; set; }
    
    public string? SlotCategory { get; set; }
    
    public string? SlotDescription { get; set; }
    
    public TimeOnly? ReservationStartedAt { get; set; }
    
    public TimeOnly? ReservationEndedAt { get; set; }
    
    public string? ReservationId { get; set; }
    
    public string? VehicleNumber { get; set; }
    
    public string? VehicleOwner { get; set; }
    
    public string? ContactNumber { get; set; }
    
    public string? SlotStatus { get; set; }
}