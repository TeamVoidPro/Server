using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Server.Models;

public class BookingPlan
{
    [Key]
    [Required(ErrorMessage = "Booking plan id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string BookingPlanId { get; set; }
    
    [Required(ErrorMessage = "Parking place id is required")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Required(ErrorMessage = "Plan duration is required")]
    public required string PlanDuration { get; set; }
    
    [Required(ErrorMessage = "Plan name is required")]
    public required string PlanName { get; set; }
    
    [Required(ErrorMessage = "Description is required")]
    public required string Description { get; set; }
    
    public ICollection<ZonePlan> ZonePlans { get; set; } = null!;
}