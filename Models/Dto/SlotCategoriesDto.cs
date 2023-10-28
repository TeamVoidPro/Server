using System.ComponentModel.DataAnnotations;

namespace Server.Models.Dto;

public class SlotCategoriesDto
{
    [Required(ErrorMessage = "Slot category name is required")]
    public required string SlotCategoryName { get; set; }
    
    public string SlotCategoryDescription { get; set; } = null!;
    
    public DateTime CategoryCreatedDate { get; set; } 
}