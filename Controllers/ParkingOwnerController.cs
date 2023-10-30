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
    public async Task<IActionResult> AddParkingOwner([FromBody] ParkingPlaceOwnerDto ownerDto)
    {
        var parkingOwner = _context.ParkingPlaceOwners!.FirstOrDefault(p => p.Email == ownerDto.Email);

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
            FirstName = ownerDto.FirstName,
            LastName = ownerDto.LastName,
            Email = ownerDto.Email,
            Password = ownerDto.Password,
            AddressLine1 = ownerDto.AddressLine1,
            Street = ownerDto.Street,
            City = ownerDto.City,
            Province = ownerDto.Province,
            LandAddressNumber = ownerDto.LandAddressNumber,
            LandAddressStreet = ownerDto.LandAddressStreet,
            LandAddressCity = ownerDto.LandAddressCity,
            LandAddressProvince = ownerDto.LandAddressProvince,
            ContactNumber = ownerDto.ContactNumber,
            DeedCopy = ownerDto.DeedCopy,
            LandMap = ownerDto.LandMap,
            LandImages = ownerDto.LandImages,
            Nic = ownerDto.Nic,
            NicFront = ownerDto.NicFront,
            NicBack = ownerDto.NicBack,
            AccountCreatedAt = ownerDto.AccountCreatedAt,
            Token = ""
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