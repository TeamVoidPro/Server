namespace Server.Models.Dto;

public class FreeSlotsResponseDto
{
    public required string SlotId { get; set; }
    
    public required int SlotNumber { get; set; }
    
    public required string ZoneId { get; set; }
    
    public required string? ZoneName { get; set; }
}