using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Reservations: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public Reservations(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("get-upcoming-reservations/{date}/{startTime}/{endTime}")]
    public async Task<IActionResult> GetUpcomingReservations(string date, string startTime, string endTime)
    {
        DateOnly.TryParse(date, out DateOnly dateOut);
        TimeOnly.TryParse(startTime, out TimeOnly startTimeOut);
        TimeOnly.TryParse(endTime, out TimeOnly endTimeOut);

        var reservationList = new List<ReservationResponseDto>();

        var upcomingReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Active"  && r.ReservationDate == dateOut && r.ReservationStartAt>=startTimeOut && r.ReservationEndAt<=endTimeOut).ToListAsync();
        foreach (var reservation in upcomingReservations)
        {
            var onsiteReservation =
                await _context.OnlineReservations!.FirstOrDefaultAsync(e =>
                    e.OnlineReservationId == reservation.ReservationId);

            var vehicle =
                await _context.Vehicles!.FirstOrDefaultAsync(v => v.VehicleNumber == onsiteReservation!.VehicleNumber);

            var driver = await _context.Drivers!.FirstOrDefaultAsync(d => d.DriverId == vehicle!.DriverId);

            var slot = await _context.Slots!.FirstOrDefaultAsync(s => s.SlotId == reservation.SlotId);

            var reservationResponseDto = new ReservationResponseDto
            {
                ReservationID = reservation.ReservationId,
                Name = driver!.FirstName+" "+driver!.LastName,
                ReservationStartedAt = reservation.ReservationStartAt,
                ReservationEndedAt = reservation.ReservationEndAt,
                ContactNumber = driver.ContactNumber,
                VehicleNumber = vehicle!.VehicleNumber,
                VehicleType = vehicle!.VehicleType,
                VehicleModel = vehicle!.VehicleModel,
                SlotNumber = slot!.SlotNumber,
                ReservationId = reservation.ReservationId
            };
            
            reservationList.Add(reservationResponseDto);
        }

        return Ok(new
        {
            reservations = reservationList
        });
    }
    
    [HttpGet("get-cancelled-reservations/{date}/{startTime}/{endTime}")]
    public async Task<IActionResult> GetCancelledReservations(string date, string startTime, string endTime)
    {
        DateOnly.TryParse(date, out DateOnly dateOut);
        TimeOnly.TryParse(startTime, out TimeOnly startTimeOut);
        TimeOnly.TryParse(endTime, out TimeOnly endTimeOut);
        
        var canceledReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Cancel"  && r.ReservationDate == dateOut && r.ReservationStartAt>=startTimeOut && r.ReservationEndAt<=endTimeOut).ToListAsync();

        return Ok(new
        {
            reservations = canceledReservations
        });
    }
    
    [HttpGet("get-completed-reservations/{date}/{startTime}/{endTime}")]
    public async Task<IActionResult> GetCompletedReservations(string date, string startTime, string endTime)
    {
        DateOnly.TryParse(date, out DateOnly dateOut);
        TimeOnly.TryParse(startTime, out TimeOnly startTimeOut);
        TimeOnly.TryParse(endTime, out TimeOnly endTimeOut);
        
        var completedReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Completed"  && r.ReservationDate == dateOut && r.ReservationStartAt>=startTimeOut && r.ReservationEndAt<=endTimeOut).ToListAsync();

        return Ok(new
        {
            reservations = completedReservations
        });
    }

    [HttpPost("make-onsite-reservation")]
    public async Task<IActionResult> MakeOnsiteReservation([FromBody] ReservationRequestDto reservationRequestDto)
    {
        var slot = await _context.Slots!.FirstOrDefaultAsync(s => s.SlotId == reservationRequestDto.SlotId);

        var reservationId = IdGenerator.GenerateId("RES");
        
        DateTime startTime = DateTimeOffset.FromUnixTimeMilliseconds(reservationRequestDto.StartingTime).DateTime;
        DateTime endTime = DateTimeOffset.FromUnixTimeMilliseconds(reservationRequestDto.EndingTime).DateTime;
        
        startTime = startTime.AddHours(5).AddMinutes(30);
        endTime = endTime.AddHours(5).AddMinutes(30);
        
        TimeOnly startedTimeOnly = new TimeOnly(startTime.Hour, startTime.Minute, startTime.Second);
        TimeOnly endedTimeOnly = new TimeOnly(endTime.Hour, endTime.Minute, endTime.Second);
        
        Reservation reservation = new Reservation
        {
            ReservationId = reservationId,
            ReservationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            ReservationStartAt = startedTimeOnly,
            ReservationEndAt = endedTimeOnly,
            ReservationStatus = "Active",
            ReservationType = "Onsite",
            SlotId = reservationRequestDto.SlotId,
            ParkingPlaceId = reservationRequestDto.ParkingPlaceId,
            PaymentMethod = reservationRequestDto.PaymentMethod,
            ZoneId = slot!.ZoneId,
            TotalPayment = 1250,
            ReservedAt = DateTime.UtcNow,
            ParkedAt = DateTime.UtcNow,
            ExitedAt = null,
            CancelledAt = null,
            PaymentStatus = "Paid",
            CancellationReason = ""
        };
        
        await _context.Reservations!.AddAsync(reservation);

        OnsiteReservations onsiteReservations = new OnsiteReservations
        {
            OnsiteReservationId = reservationId,
            ParkingPlaceOperatorId = reservationRequestDto.OperatorId,
            DriverName = reservationRequestDto.Name,
            ContactNumber = reservationRequestDto.Contact,
            VehicleNumber = reservationRequestDto.VehicleNumber,
            VehicleType = reservationRequestDto.VehicleType
        };
        
        await _context.OnsiteReservations!.AddAsync(onsiteReservations);

        slot.ReservedAt = startedTimeOnly;
        slot.ReservedUntil = endedTimeOnly;
        slot.SlotStatus = "Parked";
        slot.ReservationId = reservationId;
        
        await _context.SaveChangesAsync();
        
        return Ok(new
        {
            message = "Reservation Created Successfully.",
            data = onsiteReservations
        });
    }

    [HttpPost("calculate-reservation-cost/{slotId}/{startAt}/{endAt}")]
    public async Task<IActionResult> CalculateReservationCost(string slotId, string startAt, string endAt)
    {
        if (slotId == "")
        {
            return BadRequest(new
            {
                error = "Invalid SlotId"
            });
        }
        
        DateTime startTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(startAt)).DateTime;
        DateTime endTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(endAt)).DateTime;

        startTime = startTime.AddHours(5).AddMinutes(30);
        endTime = endTime.AddHours(5).AddMinutes(30);

        TimeOnly startedTimeOnly = new TimeOnly(startTime.Hour, startTime.Minute, startTime.Second);
        TimeOnly endedTimeOnly = new TimeOnly(endTime.Hour, endTime.Minute, endTime.Second);

        var duration = endedTimeOnly - startedTimeOnly;

        var slot = await _context.Slots!.FirstOrDefaultAsync(s => s.SlotId == slotId);
        var zone = await _context.Zones!.FirstOrDefaultAsync(z => z.ZoneId == slot!.ZoneId);

        double costPerMinute = (double)zone!.ZonePrice/60;
        var totalMinutes = duration.TotalMinutes;

        var totalCost = costPerMinute * totalMinutes;

        return Ok(new
        {
            data = duration,
            id = slotId,
            totalCost = totalCost
        });
    }

    [HttpGet("get-reservation/{id}")]
    public async Task<IActionResult> GetReservationById(string id)
    {
        var reservation = _context.Reservations!.FirstOrDefault(r => r.ReservationId == id);
        
        if (reservation == null)
        {
            return BadRequest(new
            {
                error = "Invalid Reservation Id"
            });
        }
        
        var onlineReservation = await _context.OnlineReservations!.FirstOrDefaultAsync(o => o.OnlineReservationId == id);
        var vehicle = await _context.Vehicles!.FirstOrDefaultAsync(v => v.VehicleNumber == onlineReservation!.VehicleNumber);
        var driver = await _context.Drivers!.FirstOrDefaultAsync(d => d.DriverId == vehicle!.DriverId);
        var slot = await _context.Slots!.FirstOrDefaultAsync(s => s.SlotId == reservation.SlotId);
        
        ReservationByIdResponseDto reservationByIdResponseDto = new ReservationByIdResponseDto
        {
            SlotNumber = slot!.SlotNumber,
            VehicleNumber = onlineReservation!.VehicleNumber,
            VehicleType = vehicle!.VehicleType,
            VehicleModel = vehicle!.VehicleModel,
            Name = driver!.FirstName+" "+driver!.LastName,
            ReservedAt = reservation.ReservedAt,
            ReservationStartedAt = reservation.ReservationStartAt,
            ReservationEndedAt = reservation.ReservationEndAt,
            ReservationType = reservation.ReservationType,
            ReservationAmount = reservation.TotalPayment,
            ReservationStatus = reservation.ReservationStatus,
            AdditionalNote = onlineReservation.SpecialNotes,
            ZoneName = _context.Zones!.FirstOrDefault(z => z.ZoneId == slot.ZoneId)!.ZoneName,
            SlotId = slot.SlotId,
            ReservationId = reservation.ReservationId,
            ReservationDate = reservation.ReservationDate,
            ContactNumber = driver.ContactNumber,
            PaymentMethod = reservation.PaymentMethod,
            PaymentStatus = reservation.PaymentStatus
        };
        
        return Ok(new
        {
            data = reservationByIdResponseDto
        });
    }

}