using Microsoft.AspNetCore.Mvc;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class ParkingOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ParkingOwnerController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("add-parking-owner")]
    public async Task<IActionResult> AddParkingOwner([FromBody] ParkingPlaceOwnerDto parkingOwnerDto)
    {
        var parkingOwner = _context.ParkingPlaceOwners!.FirstOrDefault(p => p.Email == parkingOwnerDto.Email);

        if (parkingOwner != null)
        {
            return BadRequest(new
            {
                message = "Parking Owner already exists"
            });
        }

        var newParkingOwner = new ParkingPlaceOwner()
        {
            OwnerId = IdGenerator.GenerateId("OWN"),
            FullName = parkingOwnerDto.FullName,
            Email = parkingOwnerDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(parkingOwnerDto.Password),
            AddressLine1 = parkingOwnerDto.AddressLine1,
            AddressLine2 = parkingOwnerDto.AddressLine2,
            Street = parkingOwnerDto.Street,
            City = parkingOwnerDto.City,
            ContactNumber = parkingOwnerDto.ContactNumber,
            Nic = parkingOwnerDto.Nic,
            DeedCopy = parkingOwnerDto.DeedCopy,
            Token = parkingOwnerDto.Token,
            AccountCreatedAt = DateTime.UtcNow
        };

        await _context.ParkingPlaceOwners!.AddAsync(newParkingOwner);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Parking Owner added successfully",
            data = newParkingOwner
        });
    }
}