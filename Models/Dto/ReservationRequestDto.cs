namespace Server.Models.Dto;

public class ReservationRequestDto
{
    public string Name { get; set; } = null!;
    
    public string Contact { get; set; } = null!;
    
    public string VehicleNumber { get; set; } = null!;
    
    public string VehicleType { get; set; } = null!;
    
    public long StartingTime { get; set; }
    
    public long EndingTime { get; set; }
    
    public string Zone { get; set; } = null!;
    
    public string PaymentMethod { get; set; } = null!;
    
    public string SlotId { get; set; } = null!;
    
    public string ParkingPlaceId { get; set; } = null!;

    public string OperatorId { get; set; } = null!;

    public decimal TotalPayment { get; set; }
}