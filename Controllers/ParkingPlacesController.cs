using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkingPlacesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ParkingPlacesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("add-parking-place")]
    public async Task<IActionResult> AddParkingPlace([FromBody] ParkingPlaceDto parkingPlaceDto)
    {
        var parkingPlace = _context.ParkingPlaces!.FirstOrDefault(p => p.Name == parkingPlaceDto.Name);

        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Invalid data provided"
            });
        }
        
        if (parkingPlace != null)
        {
            return BadRequest(new
            {
                message = "Parking place name already exists."
            });
        }

        var newParkingPlace = new ParkingPlace
        {
            ParkingPlaceId = IdGenerator.GenerateId("PARK"),
            Name = parkingPlaceDto.Name,
            Location = parkingPlaceDto.Location,
            Description = parkingPlaceDto.Description,
            ParkingPlaceOwnerId = parkingPlaceDto.ParkingPlaceOwnerId,
            ParkingPlaceVerifierId = parkingPlaceDto.ParkingPlaceVerifierId,
            ParkingPlaceOperatorId = parkingPlaceDto.ParkingPlaceOperatorId,
        };

        await _context.ParkingPlaces!.AddAsync(newParkingPlace);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Parking place added successfully",
            data = newParkingPlace
        });
    }

    [HttpGet("get-parking-place/{parkingPlaceId}")]
    public async Task<IActionResult> GetParkingPlace(string parkingPlaceId)
    {
        var parkingPlace = await _context.ParkingPlaces!.FirstOrDefaultAsync(p => p.ParkingPlaceId == parkingPlaceId);

        if (parkingPlace == null)
        {
            return NotFound(new
            {
                message = "Parking place not found"
            });
        }

        return Ok(new
        {
            data = parkingPlace
        });
        
    }

}