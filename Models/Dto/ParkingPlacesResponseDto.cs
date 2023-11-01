using System.Runtime.InteropServices.JavaScript;

namespace Server.Models.Dto;

public class ParkingPlacesResponseDto
{
    public string? ParkingPlaceId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Location { get; set; }
    
    public string? ParkingOperator { get; set; }
    
    public string? ParkingOwner { get; set; }
    
    public string? ParkingVerifier { get; set; }
    
    public DateTime? Date { get; set; }
    
    
    
}