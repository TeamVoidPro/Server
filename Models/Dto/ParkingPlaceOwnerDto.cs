
namespace Server.Models.Dto;

public class ParkingPlaceOwnerDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string ConfirmPassword { get; set; }

    public required string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public required string City { get; set; }

    public required string ContactNumber { get; set; }

    public required string NIC { get; set; }
    
    // Get the NIC Front Image as a File
    public required string NICFrontImage { get; set; }
    
    // Get the NIC Back Image as a File
    public required string NICBackImage { get; set; }

    
}