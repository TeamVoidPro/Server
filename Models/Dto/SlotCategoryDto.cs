namespace Server.Models.Dto;

public class SlotCategoryDto
{
    public required string SlotCategoryId { get; set; }
    
    public required string SlotCategoryName { get; set; }
    
    public string? SlotCategoryDescription { get; set; }
    
    public DateTime CategoryCreatedDate { get; set; } = DateTime.Now;
}