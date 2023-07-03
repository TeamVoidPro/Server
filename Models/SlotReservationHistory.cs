using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class SlotReservationHistory
{
    [Key]
    [Required(ErrorMessage = "Slot reservation history id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string SlotReservationHistoryId { get; set; }
    
    [Required(ErrorMessage = "Slot reservation id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ReservationId { get; set; }
    
    public Reservation Reservation { get; set; } = null!;
    
    [Required(ErrorMessage = "Slot reservation time is required")]
    public required DateTime ReservationTime { get; set; }
    
    [Required(ErrorMessage = "Slot reservation start time is required")]
    public required DateTime ReservationStartTime { get; set; }
    
    [Required(ErrorMessage = "Slot reservation end time is required")]
    public required DateTime ReservationEndTime { get; set; }
    
    [Required(ErrorMessage = "Slot id is required")]
    public required string SlotId { get; set; }
    
    public Slot Slot { get; set; } = null!;
}