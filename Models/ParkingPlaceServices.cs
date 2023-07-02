using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Index(nameof(ParkingPlaceId), nameof(ServiceProvide), IsUnique = true)]
public class ParkingPlaceServices
{
    [ForeignKey("ParkingPlace")]
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Required(ErrorMessage = "Service provide is required")]
    public required string ServiceProvide { get; set; }
}