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
    
    [HttpGet("get-free-slots-by-time-duration/{parkingPlaceId}/{startAt}/{endAt}")]
    public IActionResult GetFreeSlotsByTimeDuration(string parkingPlaceId, string startAt, string endAt)
    {
        TimeOnly.TryParse(startAt, out TimeOnly startTimeOut);
        TimeOnly.TryParse(endAt, out TimeOnly endTimeOut);
        
        var freeSlots = _context.Slots!.Where(s =>
            s.ParkingPlaceId == parkingPlaceId && s.SlotStatus == "Available")
            .Select(s => new
            {
                s.SlotId,
                s.SlotNumber,
                s.Reservations,
                s.ZoneId
            });

        var filteredSlots = new List<FreeSlotsResponseDto>();
        var flag = true;
        
        foreach (var slot in freeSlots)
        {
            foreach (var reservation in slot.Reservations)
            {
                if(reservation.ReservationStartAt >= startTimeOut && reservation.ReservationEndAt <= endTimeOut)
                {
                    flag = false;
                }
                    
            }
            
            if (flag)
            {
                var zone = _context.Zones!.Where(z => z.ZoneId == slot.ZoneId);
                filteredSlots.Add(new FreeSlotsResponseDto()
                {
                    SlotId = slot.SlotId,
                    SlotNumber = slot.SlotNumber,
                    ZoneId = slot.ZoneId,
                    ZoneName = zone.Select(z => z.ZoneName).FirstOrDefault()
                    
                });
            }
        }
        
        return Ok(new
        {
            slots = filteredSlots
        });
    }
}