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
    
    [HttpPost("register")]
    public async Task<IActionResult> AddParkingOwner([FromBody] ParkingPlaceOwnerDto parkingOwnerDto)
    {
        var parkingOwner = _context.ParkingPlaceOwners!.FirstOrDefault(p => p.Email == parkingOwnerDto.Email);

        if (parkingOwner != null)
        {
            return BadRequest(new
            {
                message = "Email is Already Exists !"
            });
        }

        var newParkingOwner = new ParkingPlaceOwner()
        {
            OwnerId = IdGenerator.GenerateId("OWN"),
            FirstName = parkingOwnerDto.FirstName,
            LastName = parkingOwnerDto.LastName,
            Email = parkingOwnerDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(parkingOwnerDto.Password),
            AddressLine1 = parkingOwnerDto.AddressLine1,
            AddressLine2 = parkingOwnerDto.AddressLine2,
            City = parkingOwnerDto.City,
            ContactNumber = parkingOwnerDto.ContactNumber,
            NIC = parkingOwnerDto.NIC,
            Token = "",
            AccountCreatedAt = DateTime.UtcNow,
            NICBack = "Back",
            NICFront = ""
        };

        await _context.ParkingPlaceOwners!.AddAsync(newParkingOwner);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Parking Owner added successfully",
            data = newParkingOwner
        });
    }
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return Content("file not selected");
        // console log the file name
        Console.WriteLine(file.FileName);
        
        // Generate unique file name
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        var path = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot",
            fileName
            );

        await using (var stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { fileName });
    }
}