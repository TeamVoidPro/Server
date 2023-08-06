using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DbContext;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("get-employees")]
    [Authorize]
    public IActionResult Get()
    {
        var employees = _context.Employees!.Where(e => e.Role != "Administrator").ToList();

        return Ok(new
        {
            emp = employees
        });
    }
}