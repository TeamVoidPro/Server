namespace Server.Models.Dto;

public class GetSlotsByTimeDto
{
    public required string ParkingPlaceId { get; set; }
    
    public required DateTime ReservationStartsAt { get; set; }
    
    public required DateTime ReservationEndsAt { get; set; }
}