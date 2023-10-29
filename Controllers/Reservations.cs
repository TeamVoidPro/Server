using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;

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
        
        var upcomingReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Active"  && r.ReservationDate == dateOut && r.ReservationStartAt<=startTimeOut && r.ReservationEndAt>=endTimeOut).ToListAsync();

        return Ok(new
        {
            reservations = upcomingReservations
        });
    }
    
    [HttpGet("get-cancelled-reservations/{date}/{startTime}/{endTime}")]
    public async Task<IActionResult> GetCancelledReservations(string date, string startTime, string endTime)
    {
        DateOnly.TryParse(date, out DateOnly dateOut);
        TimeOnly.TryParse(startTime, out TimeOnly startTimeOut);
        TimeOnly.TryParse(endTime, out TimeOnly endTimeOut);
        
        var canceledReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Cancel"  && r.ReservationDate == dateOut && r.ReservationStartAt<=startTimeOut && r.ReservationEndAt>=endTimeOut).ToListAsync();

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
        
        var completedReservations = await _context.Reservations.Where(r => r.ReservationStatus == "Completed"  && r.ReservationDate == dateOut && r.ReservationStartAt<=startTimeOut && r.ReservationEndAt>=endTimeOut).ToListAsync();

        return Ok(new
        {
            reservations = completedReservations
        });
    }
    
   
}