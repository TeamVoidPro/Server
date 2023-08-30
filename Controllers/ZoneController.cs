using Microsoft.AspNetCore.Mvc;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ZoneController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ZoneController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("create-zone")]
    public async Task<IActionResult> CreateZone([FromBody] ZoneDto zoneDto)
    {
        var zone = _context.Zones!.FirstOrDefault(z => z.ZoneName == zoneDto.ZoneName && z.ParkingPlaceId == zoneDto.ParkingPlaceId);

        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Invalid data provided"
            });
        }
        
        if (zone != null)
        {
            return BadRequest(new
            {
                message = "Zone name already exists for the parking place."
            });
        }

        var newZone = new Zones
        {
            ZoneId = IdGenerator.GenerateId("ZONE"),
            ZoneName = zoneDto.ZoneName,
            ZonePrice = zoneDto.ZonePrice,
            ZoneDescription = zoneDto.ZoneDescription,
            ParkingPlaceId = zoneDto.ParkingPlaceId,
            ZoneCreatedDate = DateTime.UtcNow
        };

        await _context.Zones!.AddAsync(newZone);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Zone added successfully",
            data = newZone
        });
    }
    
    [HttpGet("get-zones-by-parking-place/{parkingPlaceId}")]
    public IActionResult GetZonesByParkingPlace(string parkingPlaceId)
    {
        var zones = _context.Zones!.Where(z => z.ParkingPlaceId == parkingPlaceId);

        if (zones == null)
        {
            return NotFound(new
            {
                message = "No zones found for the parking place."
            });
        }

        return Ok(new
        {
            message = "Zones found successfully",
            data = zones
        });
    }
}