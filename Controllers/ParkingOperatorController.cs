using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkingOperatorController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ParkingOperatorController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("get-operator-parking")]
    [Authorize]
    public async Task<IActionResult> GetOperatorParking(string parkingOperatorId)
    {
        var parkingPlace = await _context.ParkingPlaces!.FirstOrDefaultAsync(p => p.ParkingPlaceOperatorId == parkingOperatorId);

        if (parkingPlace == null)
        {
            return NotFound(new
            {
                message = "Parking place not found"
            });
        }
        
        return Ok(new
        {
            parking = parkingPlace
        });
    }
}