using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Server.Models;

[Index(nameof(ParkingPlaceId), nameof(Name), nameof(Location), nameof(ParkingPlaceOperatorId),
    nameof(ParkingPlaceVerifierId), IsUnique = true)]
public class ParkingPlace
{
    [Key]
    [Required(ErrorMessage = "Parking place id is required")]
    [Column(TypeName = "varchar(20)")]
    public required string ParkingPlaceId { get; set; }

    [Required(ErrorMessage = "Parking place name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The parking place name must be 2 characters long.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Parking place location is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "The parking place location must be 2 characters long.")]
    public required string Location { get; set; }

    public string Description { get; set; } = null!;

    public required ParkingPlaceOwner ParkingPlaceOwner { get; set; } = null!;

    [ForeignKey("Employee")] public string? ParkingPlaceVerifierId { get; set; }

    public Employee ParkingPlaceVerifier { get; set; } = null!;

    [ForeignKey("Employee")] public string? ParkingPlaceOperatorId { get; set; }

    public Employee ParkingPlaceOperator { get; set; } = null!;

    public ICollection<ParkingPlaceImages> ParkingPlaceImages { get; set; } = null!;

    public ICollection<ParkingPlaceServices> ParkingPlaceServices { get; set; } = null!;

    public ICollection<ParkingPlaceRatings> ParkingPlaceRatings { get; set; } = null!;

    public ICollection<ParkingPlaceSlotCapacities> ParkingPlaceSlotCapacities { get; set; } = null!;

    public ICollection<Zones> Zones { get; set; } = null!;

    public ICollection<Slot> Slots { get; set; } = null!;

    public ICollection<Reservation> Reservations { get; set; } = null!;
    
    public ICollection<BookingPlan> BookingPlans { get; set; } = null!;
}