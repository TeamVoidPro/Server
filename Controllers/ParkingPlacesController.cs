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
    
    [HttpGet("get-all-parking-places")]
    public async Task<IActionResult> GetAllParkingPlaces()
    {
        var parkingPlaces = await _context.ParkingPlaces!.ToListAsync();
       
        if (parkingPlaces == null)
        {
            return NotFound(new
            {
                message = "Parking place not found"
            });
        }

        var parkingPlacesList = new List<ParkingPlacesResponseDto>();

        foreach (var parkingPlace in parkingPlaces)
        {
            var parkingOwner =
                await _context.ParkingPlaceOwners!.FirstOrDefaultAsync(
                    p => p.OwnerId == parkingPlace.ParkingPlaceOwnerId);
            var parkingOperator =
                await _context.Employees!.FirstOrDefaultAsync(p => p.EmployeeId == parkingPlace.ParkingPlaceOperatorId);
            var parkingVerifier =
                await _context.Employees!.FirstOrDefaultAsync(p => p.EmployeeId == parkingPlace.ParkingPlaceVerifierId);

            var parkingPlaceResponseDto = new ParkingPlacesResponseDto
            {
                ParkingPlaceId = parkingPlace.ParkingPlaceId,
                Name = parkingPlace.Name,
                Location = parkingPlace.Location,
                ParkingOperator = parkingOperator!.FirstName+" "+ parkingOperator.LastName,
                ParkingOwner = parkingOwner!.FullName,
                ParkingVerifier = parkingVerifier!.FirstName+" "+ parkingVerifier.LastName
            };
            parkingPlacesList.Add(parkingPlaceResponseDto);
        }

        return Ok(new
        {
            data = parkingPlacesList
        });
    }
    
    [HttpGet("get-parking-place-by-operator/{operatorId}")]
    public async Task<IActionResult> GetParkingPlaceByOperator(string operatorId)
    {
        var parkingPlace = await _context.ParkingPlaces!.FirstOrDefaultAsync(p => p.ParkingPlaceOperatorId == operatorId);

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