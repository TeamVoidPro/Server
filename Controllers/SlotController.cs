using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlotController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public SlotController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("add-slot")]
    public async Task<IActionResult> AddSlot([FromBody] SlotDto slotDto)
    {
        var slot = _context.Slots!.FirstOrDefault(s => s.SlotNumber == slotDto.SlotNumber && s.ZoneId == slotDto.ZoneId && s.ParkingPlaceId == slotDto.ParkingPlaceId);

        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Invalid data provided"
            });
        }
        
        if (slot != null)
        {
            return BadRequest(new
            {
                message = "Slot number already exists."
            });
        }

        var newSlot = new Slot
        {
            SlotId = IdGenerator.GenerateId("SLOT"),
            SlotNumber = slotDto.SlotNumber,
            SlotCategoryId = slotDto.SlotCategoryId,
            ZoneId = slotDto.ZoneId,
            ParkingPlaceId = slotDto.ParkingPlaceId,
            IsAvailable = slotDto.IsAvailable,
            SlotStatus = slotDto.SlotStatus,
            Description = slotDto.Description,
            SlotCreatedDate = DateTime.UtcNow,
            ReservedAt = slotDto.ReservedAt,
            ReservedUntil = slotDto.ReservedUntil
        };

        await _context.Slots!.AddAsync(newSlot);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Slot added successfully",
            data = newSlot
        });
    }
    
    [HttpGet("get-slots-by-parking-place/{parkingPlaceId}")]
    public IActionResult GetSlotsByParkingPlace(string parkingPlaceId)
    {
        var slots = from zones in _context.Zones
            join slot in _context.Slots on zones.ZoneId equals slot.ZoneId
            where zones.ParkingPlaceId == parkingPlaceId
            select new
            {
                slot.SlotId,
                slot.SlotNumber,
                slot.SlotCategoryId,
                slot.ZoneId,
                slot.ParkingPlaceId,
                slot.IsAvailable,
                slot.SlotStatus,
                slot.Description,
                slot.SlotCreatedDate,
                slot.ReservedAt,
                slot.ReservedUntil,
                zones.ZoneName
            };

        if (slots == null)
        {
            return NotFound(new
            {
                message = "Slots not found"
            });
        }
        
        return Ok(new
        {
            data = slots
        });
    }
    
    [HttpGet("get-slot-by-zone/{zoneId}")]
    public IActionResult GetSlotByZone(string zoneId)
    {
        var slots = _context.Slots!.Where(s => s.ZoneId == zoneId).OrderBy(s => s.SlotNumber)
            .Select(
                s => new
                {
                    s.SlotId,
                    s.SlotNumber,
                    s.SlotCategoryId,
                    s.ParkingPlaceId,
                    s.IsAvailable,
                    s.SlotStatus,
                    s.Description,
                    s.SlotCreatedDate,
                    s.ReservedAt,
                    s.ReservedUntil
                });
        
        if (slots == null)
        {
            return NotFound(new
            {
                message = "Slots not found"
            });
        }
        
        return Ok(new
        {
            data = slots
        });
    }
}