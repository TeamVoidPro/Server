using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models;

public class IssueImages
{
    [Column(TypeName = "varchar(20)")]
    public required string IssueId { get; set; }
    
    public Issues Issues { get; set; } = null!;
    
    public required string Image { get; set; }
}