namespace Server.Models.Dto;

public class ReservationByIdResponseDto
{
    public string? SlotId { get; set; }
    public int? SlotNumber { get; set; }
    
    public string? VehicleNumber { get; set; }
    
    public string? VehicleType { get; set; }
    
    public string? VehicleModel { get; set; }
    
    public string? Name { get; set; }
    
    public DateTime? ReservedAt { get; set; }
    
    public TimeOnly? ReservationStartedAt { get; set; }
    
    public TimeOnly? ReservationEndedAt { get; set; }
    
    public string? ReservationType { get; set; }
    
    public decimal? ReservationAmount { get; set; }
    
    public string? ReservationStatus { get; set; }
    
    public string? AdditionalNote { get; set; }
    
    public string? ZoneName { get; set; }
    
    public string? ReservationId { get; set; }
    
    public DateOnly? ReservationDate { get; set; }
    
    public string? ContactNumber { get; set; }
    
    public string? PaymentMethod { get; set; }
    
    public string? PaymentStatus { get; set; }
    
}