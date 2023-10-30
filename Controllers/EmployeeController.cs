using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.DbContext;
using Server.Models.Dto;

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

    [HttpGet("get-employees/{employeeId}")]
    public IActionResult GetEmployeeById(string employeeId)
    {
        var employee = _context.Employees!.FirstOrDefault(e => e.EmployeeId == employeeId);

        if (employee == null)
        {
            return NotFound(new
            {
                message = "Employee not found"
            });
        }

        return Ok(new
        {
            emp = employee
        });
    }
    
    [HttpPost("update-employee")]
    [Authorize]
    public IActionResult UpdateEmployee(EmployeeIdDto employeeIdDto)
    {
        var employee = _context.Employees!.FirstOrDefault(e => e.EmployeeId == employeeIdDto.EmployeeId);

        if (employee == null)
        {
            return NotFound(new
            {
                message = "Employee not found"
            });
        }

        employee.Role = employee.Role == "Employee" ? "Manager" : "Employee";
        _context.SaveChanges();

        return Ok(new
        {
            message = "Employee updated successfully"
        });
    }
    
    [HttpPost("delete-employee")]
    [Authorize]
    public IActionResult DeleteEmployee(EmployeeIdDto employeeIdDto)
    {
        var employee = _context.Employees!.FirstOrDefault(e => e.EmployeeId == employeeIdDto.EmployeeId);

        if (employee == null)
        {
            return NotFound(new
            {
                message = "Employee not found"
            });
        }

        _context.Employees!.Remove(employee);
        _context.SaveChanges();

        return Ok(new
        {
            message = "Employee deleted successfully"
        });
    }
    
    // [HttpPost("update-employee")]
    // public IActionResult UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
    // {
    //     var employee = _context.Employees!.FirstOrDefault(e => e.EmployeeId == updateEmployeeDto.EmployeeId);
    //
    //     if (employee == null)
    //     {
    //         return BadRequest(new
    //         {
    //             message = "Employee not found"
    //         });
    //     }
    //
    //     employee.FirstName = updateEmployeeDto.FirstName;
    //     employee.LastName = updateEmployeeDto.LastName;
    //     employee.AddressLine1 = updateEmployeeDto.AddressLine1;
    //     employee.Street = updateEmployeeDto.Street;
    //     employee.City = updateEmployeeDto.City;
    //     employee.ContactNumber = updateEmployeeDto.ContactNumber;
    //     employee.Nic = updateEmployeeDto.Nic;
    //
    //     _context.Update(employee);
    //     _context.SaveChanges();
    //
    //     return Ok(new
    //     {
    //         message = "Employee Updated Successfully"
    //     });
    // }
}