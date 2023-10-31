using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Server.Models;

public class AwaitedParkingPlaces
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "AwaitedParkingPlacesId is required")]
    public required string AwaitedParkingPlacesId { get; set; }
    
    [Required(ErrorMessage = "Address Line 1 is required")]
    public required string AddressLine1 { get; set; }
    
    [Required(ErrorMessage = "Street is required")]
    public required string Street { get; set; }
    
    [Required(ErrorMessage = "City is required")]
    public required string City { get; set; }
    
    [Required(ErrorMessage = "Province is required")]
    public required string Province { get; set; }
    
    [Required(ErrorMessage = "Deed copy is required")]
    public required string DeedCopy { get; set; }
    
    public string LandMapImage { get; set; } = null!;
    
    public string LandImages { get; set; } = null!;
    
    public double Longitude { get; set; }  
    
    public double Latitude { get; set; } 
    
    public required string ConfirmationStatus { get; set; }
    
    public DateTime? ConfirmationDate { get; set; }
    
    public required string ConfirmationReport { get; set; }
    
    public DateTime? RegistrationDate { get; set; }
    
    public string RejectionReason { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public required string OwnerId { get; set; }
    
    public required ParkingPlaceOwner ParkingPlaceOwner { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    [AllowNull]
    public string ParkingPlaceVerifierId { get; set; }
    
    public Employee ParkingPlaceVerifier { get; set; } = null!;
    
    public DateTime? InspectionDate { get; set; }
    
    public required string ParkAvailable { get; set; }
    
    public required string ParkName { get; set; }
    
    public string? ParkDescription { get; set; }
    
    public required double ParkWidth { get; set; }
    
    public required double ParkLength { get; set; }
    
    public required string ParkCategory { get; set; }
    
    public required int NoOfStories { get; set; }
    
    public required bool HasUndergroundParking { get; set; }
    
    public required string ParkImage { get; set; }

    public ICollection<AwaitedParkingPlaceSlotCapacities> AwaitedParkingPlaceSlotCapacities { get; set; } = null!;

}