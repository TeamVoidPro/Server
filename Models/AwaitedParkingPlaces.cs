using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    
    [Required(ErrorMessage = "Deed copy is required")]
    public required string DeedCopy { get; set; }
    
    public required string ConfirmationStatus { get; set; }
    
    public DateTime ConfirmationDate { get; set; }
    
    public required string ConfirmationReport { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public string RejectionReason { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public required string OwnerId { get; set; }
    
    public required ParkingPlaceOwner ParkingPlaceOwner { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public string ParkingPlaceVerifierId { get; set; } = null!;
    
    public Employee ParkingPlaceVerifier { get; set; } = null!;
    
    public DateTime InspectionDate { get; set; }
}