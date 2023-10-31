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
    
    [HttpPost("register-parking-owner")]
    public async Task<IActionResult> AddParkingOwner([FromBody] ParkingPlaceOwnerDto parkingOwnerDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Invalid Parking Owner data"
            });
        }
        
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
            City = parkingOwnerDto.City,
            Street = parkingOwnerDto.Province,
            ContactNumber = parkingOwnerDto.ContactNumber,
            Nic = parkingOwnerDto.Nic,
            IdentificationMethod = parkingOwnerDto.IdentificationMethod,
            IdentificationFrontImage = parkingOwnerDto.IdentificationFrontImage,
            IdentificationBackImage = parkingOwnerDto.IdentificationBackImage,
            AccountCreatedAt = DateTime.UtcNow,
            Token = ""
        };

        var awaitedParkingPlace = new AwaitedParkingPlaces
        {
            AwaitedParkingPlacesId = IdGenerator.GenerateId("AWP"),
            AddressLine1 = parkingOwnerDto.LandAddress1,
            Street = parkingOwnerDto.LandStreet,
            City = parkingOwnerDto.LandCity,
            Province = parkingOwnerDto.LandProvince,
            DeedCopy = parkingOwnerDto.LandDeedImage,
            LandMapImage = parkingOwnerDto.LandMapImage,
            LandImages = parkingOwnerDto.LandImages,
            Longitude = parkingOwnerDto.Longitude,
            Latitude = parkingOwnerDto.Latitude,
            ConfirmationStatus = "Pending",
            ConfirmationDate = null,
            ConfirmationReport = "",
            RegistrationDate = null,
            RejectionReason = "",
            OwnerId = newParkingOwner.OwnerId,
            ParkingPlaceOwner = newParkingOwner,
            ParkingPlaceVerifierId = null,
            InspectionDate = null,
            ParkAvailable = parkingOwnerDto.ParkAvailable,
            ParkName = parkingOwnerDto.ParkName,
            ParkDescription = parkingOwnerDto.ParkDescription,
            ParkWidth = parkingOwnerDto.ParkWidth,
            ParkLength = parkingOwnerDto.ParkLength,
            ParkCategory = parkingOwnerDto.ParkCategory,
            NoOfStories = parkingOwnerDto.NoOfStories,
            HasUndergroundParking = parkingOwnerDto.HasUndergroundParking,
            ParkImage = parkingOwnerDto.ParkImage,
            
        };
        
        // var awaitedParkingPlaceSlotCapacities = new AwaitedParkingPlaceSlotCapacities()
        // {
        //     AwaitedParkingParkingPlaceId = awaitedParkingPlace.AwaitedParkingPlacesId,
        //     AwaitedParkingSlotCategoryId = IdGenerator.GenerateId("AWPC"),
        //     // AwaitedParkingSlotCapacity = parkingOwnerDto.SlotCapacity1
        // };

        await _context.ParkingPlaceOwners!.AddAsync(newParkingOwner);
        await _context.AwaitedParkingPlaces!.AddAsync(awaitedParkingPlace); 
        // await _context.AwaitedParkingPlaceSlotCapacities!.AddAsync(awaitedParkingPlaceSlotCapacities);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Parking Owner and park details added successfully",
        });
    }
}