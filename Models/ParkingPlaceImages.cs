using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Index(nameof(ParkingPlaceId), nameof(ImageUrl), IsUnique = true)]
public class ParkingPlaceImages
{
    [ForeignKey("ParkingPlace")]
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceId { get; set; }
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    [Required(ErrorMessage = "Image is required")]
    public required string ImageUrl { get; set; }
    
}