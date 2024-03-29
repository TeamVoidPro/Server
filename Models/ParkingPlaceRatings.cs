using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class ParkingPlaceRatings
{
    [Key]
    [Required(ErrorMessage = "Parking place rating id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string RatingId { get; set; }
    
    [Required(ErrorMessage = "Rating is required")]
    [Range(0, 5, ErrorMessage = "The rating must be between 0 and 5.")]
    public required int Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime RatingDate { get; set; } = DateTime.Now;

    public string ParkingPlaceId { get; set; } = null!;
    
    public ParkingPlace ParkingPlace { get; set; } = null!;
    
    public string DriverId { get; set; } = null!;
    
    public Driver Driver { get; set; } = null!;
}