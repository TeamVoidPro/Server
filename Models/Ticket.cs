using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class Ticket
{
    [Key]
    [Column (TypeName = "varchar(20)")]
    [Required(ErrorMessage = "Ticket id is required")]
    public required string TicketId { get; set; }
    
    [Required(ErrorMessage = "QR code is required")]
    public required string QrCode { get; set; }
    
    [Required(ErrorMessage = "Validity is required")]
    public required string Validity { get; set; }
    
    public DateTime TicketCreatedDate { get; set; } = DateTime.Now;
    
    public DateTime TicketExpiredDate { get; set; }
    
    public string VerifiedBy { get; set; } = null!;
    
    public Employee ParkingPlaceOperator { get; set; } = null!;
    
    public DateTime VerifiedAt { get; set; }
}