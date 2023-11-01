using System.Runtime.InteropServices.JavaScript;
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
    
    [HttpGet("get-free-slots-by-time-duration/{parkingPlaceId}/{startAt}/{endAt}/{vehicleType}")]
    public IActionResult GetFreeSlotsByTimeDuration(string parkingPlaceId, string startAt, string endAt, string vehicleType)
    {
        DateTime startTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(startAt)).DateTime;
        DateTime endTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(endAt)).DateTime;

        startTime = startTime.AddHours(5).AddMinutes(30);
        endTime = endTime.AddHours(5).AddMinutes(30);

        TimeOnly startedTimeOnly = new TimeOnly(startTime.Hour, startTime.Minute, startTime.Second);
        TimeOnly endedTimeOnly = new TimeOnly(endTime.Hour, endTime.Minute, endTime.Second);
        
        var vehicleTypeObj = _context.SlotCategories!.FirstOrDefault(s => s.SlotCategoryName == vehicleType);

        var freeSlots = _context.Slots!.Where(s => 
            s.SlotStatus == "Available" && s.SlotCategoryId == vehicleTypeObj!.SlotCategoryId && s.ParkingPlaceId == parkingPlaceId
        ).ToList();

        var filteredSlotList = new List<FreeSlotsResponseDto>();
        var zoneList = new List<string>();
        //get free slots based on startDateTime and endDateTime
        foreach (var slot in freeSlots)
        {
            var reservations = _context.Reservations!
                .Where(r => r.SlotId == slot.SlotId && r.ReservationStatus == "Active" && r.ReservationDate == DateOnly.FromDateTime(DateTime.UtcNow)).ToList();

            bool flag = true;

            foreach (var reservation in reservations)
            {
                if(reservation.ReservationStartAt <= startedTimeOnly && reservation.ReservationEndAt >= endedTimeOnly)
                {
                    flag = false;
                }
            }

            if (flag)
            {
                FreeSlotsResponseDto freeSlotsResponseDto = new FreeSlotsResponseDto
                {
                    SlotId = slot.SlotId,
                    SlotNumber = slot.SlotNumber,
                    ZoneId = slot.ZoneId,
                    ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName
                };

                if (!zoneList.Contains(freeSlotsResponseDto.ZoneName))
                {
                    zoneList.Add(freeSlotsResponseDto.ZoneName);
                }
                    
                filteredSlotList.Add(freeSlotsResponseDto);
            }
            else
            {
                flag = true;
            }
            
        }
        
        return Ok(new
        {
            slots = filteredSlotList,
            zonesList = zoneList
        });
    }

    [HttpGet("get-distinct-slot-categories-by-parking-place/{parkingPlaceId}")]
    public async Task<IActionResult> GetAllDistinctSlotCategories(string parkingPlaceId)
    {
        var slotCategories = await _context.Slots!.Where(s => s.ParkingPlaceId == parkingPlaceId).Select(s => s.SlotCategoryId).Distinct().ToListAsync();

        if (slotCategories == null)
        {
            return NotFound(new
            {
                message = "Slot categories not found"
            });
        }

        var slotCategoriesList = new List<string>();
        
        foreach (var slot in slotCategories)
        {
            var slotCategory = await _context.SlotCategories!.FirstOrDefaultAsync(s => s.SlotCategoryId == slot);
            
            if (slotCategory != null)
            {
                slotCategoriesList.Add(slotCategory.SlotCategoryName);
            }
        }
        
        return Ok(new
        {
            categories = slotCategoriesList
        });
    }

    [HttpGet("get-slot-details-by-id/{slotId}")]
    public async Task<IActionResult> GetSlotDetailsById(string slotId)
    {
        var slot = _context.Slots!.FirstOrDefault(s => s.SlotId == slotId);
        var reservation = _context.Reservations!.FirstOrDefault(r => r.ReservationId == slot!.ReservationId);
        var slotDetailsResponseDto = new SlotDetailsResponseDto();

        if (slot!.SlotStatus == "Available")
        {
            slotDetailsResponseDto.SlotId = slot.SlotId;
            slotDetailsResponseDto.SlotCategory = _context.SlotCategories!.FirstOrDefault(s => s.SlotCategoryId == slot.SlotCategoryId)!.SlotCategoryName;
            slotDetailsResponseDto.SlotDescription = slot.Description;
            slotDetailsResponseDto.ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName;
            slotDetailsResponseDto.SlotStatus = slot.SlotStatus;
        
            return Ok(new
            {
                data = slotDetailsResponseDto
            });
        }

        if (reservation!.ReservationType == "Onsite")
        {
            var onsiteReservation = _context.OnsiteReservations!.FirstOrDefault(o => o.OnsiteReservationId == reservation.ReservationId);

            if (slot!.SlotStatus == "Parked")
            {
                slotDetailsResponseDto.ReservationId = onsiteReservation!.OnsiteReservationId;
                slotDetailsResponseDto.SlotId = slot!.SlotId;
                slotDetailsResponseDto.SlotCategory = _context.SlotCategories!.FirstOrDefault(s => s.SlotCategoryId == slot.SlotCategoryId)!.SlotCategoryName;
                slotDetailsResponseDto.SlotDescription = slot.Description;
                slotDetailsResponseDto.VehicleNumber = onsiteReservation.VehicleNumber;
                slotDetailsResponseDto.ContactNumber = onsiteReservation.ContactNumber;
                slotDetailsResponseDto.VehicleOwner = onsiteReservation.DriverName;
                slotDetailsResponseDto.ReservationStartedAt = reservation.ReservationStartAt;
                slotDetailsResponseDto.ReservationEndedAt = reservation.ReservationEndAt;
                slotDetailsResponseDto.ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName;
                slotDetailsResponseDto.SlotStatus = slot.SlotStatus;
            }
            
        }

        if (reservation!.ReservationType == "Online")
        {
            var onlineReservation = _context.OnlineReservations!.FirstOrDefault(o => o.OnlineReservationId == reservation.ReservationId);
            var vehicle = _context.Vehicles!.FirstOrDefault(v => v.VehicleNumber == onlineReservation!.VehicleNumber);
            var driver = _context.Drivers!.FirstOrDefault(d => d.DriverId == vehicle!.VehicleNumber);
            
            if (slot!.SlotStatus == "Parked")
            {
                slotDetailsResponseDto.ReservationId = onlineReservation!.OnlineReservationId;
                slotDetailsResponseDto.SlotId = slot!.SlotId;
                slotDetailsResponseDto.SlotCategory = _context.SlotCategories!.FirstOrDefault(s => s.SlotCategoryId == slot.SlotCategoryId)!.SlotCategoryName;
                slotDetailsResponseDto.SlotDescription = slot.Description;
                slotDetailsResponseDto.VehicleNumber = onlineReservation.VehicleNumber;
                slotDetailsResponseDto.ContactNumber = driver!.ContactNumber;
                slotDetailsResponseDto.VehicleOwner = driver!.FirstName + " " + driver.LastName;
                slotDetailsResponseDto.ReservationStartedAt = reservation.ReservationStartAt;
                slotDetailsResponseDto.ReservationEndedAt = reservation.ReservationEndAt;
                slotDetailsResponseDto.ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName;
                slotDetailsResponseDto.SlotStatus = slot.SlotStatus;
            }
        }
        
        return Ok(new
        {
            data = slotDetailsResponseDto
        });
    }

    [HttpGet("get-reserved-slot-details/{slotId}")]
    public async Task<IActionResult> getReservedSlotDetails(string slotId)
    {
        // Get reservation that belongs to current time based on reservation table and the columns are reservationStartAt and reservationEndAt
        
        var slot = _context.Slots!.FirstOrDefault(s => s.SlotId == slotId);
        
        if (slot == null)
        {
            return NotFound(new
            {
                message = "Slot not found"
            });
        }
        
        DateTime dateToday = DateTime.UtcNow;
        TimeOnly currentTime = new TimeOnly(dateToday.Hour, dateToday.Minute, dateToday.Second);
        
        var reservation = _context.Reservations.Where(r => r.SlotId == slotId && r.ReservationDate == DateOnly.FromDateTime(DateTime.UtcNow) && r.ReservationStartAt <= currentTime && r.ReservationEndAt >= currentTime).ToList();
        
        
        if (reservation == null)
        {
            return NotFound(new
            {
                message = "Reservation not found"
            });
        }
        
        var slotDetailsResponseDto = new SlotDetailsResponseDto();
        var onlineReservation = _context.OnlineReservations!.FirstOrDefault(o => o.OnlineReservationId == reservation[0].ReservationId);
        var vehicle = _context.Vehicles!.FirstOrDefault(v => v.VehicleNumber == onlineReservation!.VehicleNumber);
        var driver = _context.Drivers!.FirstOrDefault(d => d.DriverId == vehicle!.DriverId);
        
        slotDetailsResponseDto.SlotId = slot.SlotId;
        slotDetailsResponseDto.SlotCategory = _context.SlotCategories!.FirstOrDefault(s => s.SlotCategoryId == slot.SlotCategoryId)!.SlotCategoryName;
        slotDetailsResponseDto.SlotDescription = slot.Description;
        slotDetailsResponseDto.ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName;
        slotDetailsResponseDto.SlotStatus = slot.SlotStatus;
        slotDetailsResponseDto.ReservationId = reservation[0].ReservationId;
        slotDetailsResponseDto.ReservationStartedAt = reservation[0].ReservationStartAt;
        slotDetailsResponseDto.ReservationEndedAt = reservation[0].ReservationEndAt;
        slotDetailsResponseDto.VehicleNumber = onlineReservation!.VehicleNumber;
        slotDetailsResponseDto.ContactNumber = driver!.ContactNumber;
        slotDetailsResponseDto.VehicleOwner = driver!.FirstName + " " + driver.LastName;
        
        return Ok(new
        {
            data = slotDetailsResponseDto
        });

    }
    
    [HttpPut("update-slot-state/{slotId}/{state}")]
    public async Task<IActionResult> UpdateSlotStatus(string slotId, string state)
    {
        var slot = _context.Slots!.FirstOrDefault(s => s.SlotId == slotId);

        if (slot == null)
        {
            return NotFound(new
            {
                message = "Slot not found"
            });
        }

        slot.SlotStatus = state;
        await _context.SaveChangesAsync();
        
        return Ok(new
        {
            message = "Slot status updated successfully"
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetEmergencySlotDetails()
}