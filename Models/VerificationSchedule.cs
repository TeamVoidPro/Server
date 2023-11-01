using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class VerificationSchedule
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public required string OwnerId { get; set; }

        [Column(TypeName = "varchar(512)")]
        public required string OwnerName { get; set; }

        [Column(TypeName = "varchar(512)")]
        public required string LandAddress { get; set; }

        [Column(TypeName = "varchar(20)")]
        public required DateTime Date {  get; set; }

        [Column(TypeName = "varchar(20)")]
        public required string Time {  get; set; }

        [Column(TypeName = "varchar(20)")]
        public required string Format { get; set; }

        [Column(TypeName = "varchar(20)")]
        public required string EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;

        public ParkingPlaceOwner ParkingPlaceOwner { get; set; } = null!;
    }
}
