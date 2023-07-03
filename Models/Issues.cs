using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Issues
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Issue id is required")]
    public required string IssueId { get; set; }
    
    [Required(ErrorMessage = "Reported driver is required")]
    public required string ReportedBy { get; set; }
    
    public required Driver Driver { get; set; } = null!;

    [Required(ErrorMessage = "Issue description is required")]
    public required string IssueDescription { get; set; }
    
    public DateTime ReportedDate { get; set; } = DateTime.Now;
    
    public DateTime RespondedDate { get; set; }
    
    public required string Response { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceVerifierId { get; set; }
    
    public Employee ParkingPlaceVerifier { get; set; } = null!;
    
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    public ICollection<IssueImages> IssueImages { get; set; } = null!;
}