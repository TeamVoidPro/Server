using Microsoft.AspNetCore.Mvc;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlotCategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public SlotCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("create-slot-category")]
    public async Task<IActionResult> CreateSlotCategory([FromBody] SlotCategoryDto slotCategoryDto)
    {
        var slotCategory = _context.SlotCategories!.FirstOrDefault(sc => sc.SlotCategoryName == slotCategoryDto.SlotCategoryName);

        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Invalid data provided"
            });
        }
        
        if (slotCategory != null)
        {
            return BadRequest(new
            {
                message = "Slot category name already exists."
            });
        }

        var newSlotCategory = new SlotCategories
        {
            SlotCategoryId = IdGenerator.GenerateId("SLOT_CAT"),
            SlotCategoryName = slotCategoryDto.SlotCategoryName,
            SlotCategoryDescription = slotCategoryDto.SlotCategoryDescription,
            CategoryCreatedDate = DateTime.UtcNow
        };

        await _context.SlotCategories!.AddAsync(newSlotCategory);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Slot category added successfully",
            data = newSlotCategory
        });
    }
}