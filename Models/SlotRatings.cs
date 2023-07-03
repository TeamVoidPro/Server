using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class SlotRatings
{
    [Column(TypeName = "varchar(20)")]
    public required string SlotId { get; set; }
    
    public Slot Slot { get; set; } = null!;
    
    [Column(TypeName = "varchar(20)")]
    public required string DriverId { get; set; }
    
    public Driver Driver { get; set; } = null!;
    
    public required int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime RatedDate { get; set; } = DateTime.Now;
}