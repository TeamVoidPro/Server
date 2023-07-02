using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Zones
{
    [Key]
    [Required(ErrorMessage = "Zone id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ZoneId { get; set; }
    
    [Required(ErrorMessage = "Zone name is required")]
    public required string ZoneName { get; set; }
    
    public string? ZoneDescription { get; set; }
    
    public DateTime ZoneCreatedDate { get; set; } = DateTime.Now;

    public string ParkingPlaceId { get; set; } = null!;
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
}