using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Configurations;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JWTConfig _jwtConfig;

    public AuthController(ApplicationDbContext context, IOptions<JWTConfig> jwtConfig)
    {
        _context = context;
        _jwtConfig = jwtConfig.Value;
    }
    

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] EmployeeDto employeeDto)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = await _context.Employees!.FirstOrDefaultAsync(e => e.Email == employeeDto.Email);
        if (employee != null)
        {
            return BadRequest(new {message = "Email already exists"});
        }

        var newEmployee = new Employee
        {
            EmployeeId = IdGenerator.GenerateId("EMP"),
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(employeeDto.Password),
            AddressLine1 = employeeDto.AddressLine1,
            Street = employeeDto.Street,
            City = employeeDto.City,
            ContactNumber = employeeDto.ContactNumber,
            Nic = employeeDto.Nic,
            Role = employeeDto.Role,
            ProfilePicture = "https://i.imgur.com/6VBx3io.png",
            Token = "",
            AccountCreatedAt = DateTime.UtcNow
        };

        await _context.Employees!.AddAsync(newEmployee);
        await _context.SaveChangesAsync();

        return StatusCode(201);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = await _context.Employees!.FirstOrDefaultAsync(e => e.Email == loginDto.Email);
        if (employee == null)
        {
            return BadRequest(new {message = "Invalid credentials"});
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, employee.Password))
        {
            return BadRequest(new {message = "Invalid credentials"});
        }

        employee.Token = GenerateJwtToken(employee);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            employee.EmployeeId,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.AddressLine1,
            employee.Street,
            employee.City,
            employee.ContactNumber,
            employee.Nic,
            employee.Role,
            employee.ProfilePicture,
            employee.Token,
            employee.AccountCreatedAt
        });
    }

    private string GenerateJwtToken(Employee employee)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", employee.EmployeeId),
                new Claim(JwtRegisteredClaimNames.Email, employee.Email),
                new Claim(JwtRegisteredClaimNames.Sub, employee.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, employee.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}