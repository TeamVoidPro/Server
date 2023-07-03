using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ComplianceMonitoring
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Compliance monitoring id is required")]
    public required string ComplianceMonitoringId { get; set; }
    
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "Parking place id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    public required string ComplianceStatus { get; set; }
    
    public string? Report { get; set; }
    
    public required string Feedback { get; set; }
    
    public required string ParkingPlaceVerifierId { get; set; }
    
    public Employee ParkingPlaceVerifier { get; set; } = null!;
}