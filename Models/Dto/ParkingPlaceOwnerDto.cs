namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    public required string OwnerId { get; set; }
    
    public required string FullName { get; set; }
    
    public required string Email { get; set; }
    
    public required string AddressLine1 { get; set; }
    
    public required string Street { get; set; }
    
    public required string City { get; set; }
    
    public required int ContactNumber { get; set; }
    
    public required string DeedCopy { get; set; }
    
    public required string Nic { get; set; }
}