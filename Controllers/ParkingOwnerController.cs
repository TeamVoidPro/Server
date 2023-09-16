using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/ParkOwner")]
[ApiController]
public class ParkingOwnerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ParkingOwnerController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register-park-owners")]
    public async Task<IActionResult> RegisterParkOwner([FromBody] ParkingPlaceOwnerDto ownerDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid model object");
        }

        var owner = await _context.ParkingPlaceOwners!.FirstOrDefaultAsync(e => e.Email == ownerDto.Email);
        if (owner == null)
        {
            return BadRequest("Owner object is null");
        }

        var newOwner = new ParkingPlaceOwner
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

        await _context.ParkingPlaceOwners.AddAsync(newOwner);
        await _context.SaveChangesAsync();

        return Ok(new 
            { message = "Parking Owner added successfully" ,
                data = newOwner
                
            });
    }

}