using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class OnlineReservations
{
    [Key]
    [Column(TypeName = "varchar(20)")]
    public required string OnlineReservationId { get; set; }
    
    public Reservation Reservation { get; set; } = null!;
    
    public string SpecialNotes { get; set; } = null!;
    
    public required string VehicleNumber { get; set; }
    
    public Vehicle Vehicle { get; set; } = null!;
}