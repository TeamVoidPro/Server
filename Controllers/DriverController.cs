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
public class DriverController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JWTConfig _jwtConfig;
    private readonly TokenValidationParameters _tokenValidationParameters;
    
    public DriverController(ApplicationDbContext context, IOptions<JWTConfig> jwtConfig, TokenValidationParameters tokenValidationParameters)
    {
        _context = context;
        _jwtConfig = jwtConfig.Value;
        _tokenValidationParameters = tokenValidationParameters;
    }
    
    
    private async Task<AuthResults> GenerateJwtToken(Driver driver)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", driver.DriverId),
                new Claim(JwtRegisteredClaimNames.Email, driver.Email),
                new Claim(JwtRegisteredClaimNames.Sub, driver.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenResult = tokenHandler.WriteToken(token);

        var refreshToken = new DriverRefreshToken()
        {
            Id = IdGenerator.GenerateRandomId(60),
            DriverId = driver.DriverId,
            Token = RandomString(20) + Guid.NewGuid(),
            Expires = DateTime.UtcNow.AddMonths(6),
            IsUsed = false,
            IsRevoked = false,
            JwtId = token.Id
        };
        
        await _context.DriverRefreshTokens!.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResults()
        {
            Token = tokenResult,
            RefreshToken = refreshToken.Token,
            Result = true,
            Errors = null
        };
    }
    
    private string RandomString(int length)
    { 
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz_"; 
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] DriverDto driver)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingDriver = await _context.Drivers!.FirstOrDefaultAsync(d => d.Email == driver.Email);
        if (existingDriver != null)
        {
            return BadRequest(
                new
                {
                    message = "Driver with this email already exists"
                }
                );
        }

        if (driver.Password != driver.ConfirmPassword)
        {
            return BadRequest(
            new{
                message ="Password and confirm password do not match"
            });

        }
        
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(driver.Password);
        var newDriver = new Driver
        {
            DriverId = IdGenerator.GenerateId("Drv"),
            Email = driver.Email,
            Password = hashedPassword,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            ContactNumber = driver.ContactNumber,
            AccountCreatedAt = DateTime.UtcNow,
            Token = ""
        };
        
        

        await _context.Drivers!.AddAsync(newDriver);
        await _context.SaveChangesAsync();
        var jwtToken = await GenerateJwtToken(newDriver);
        newDriver.Token = jwtToken.Token;
        await _context.SaveChangesAsync();
        
        var user = new
        {
            Id = newDriver.DriverId,
            FirstName = newDriver.FirstName,
            LastName = newDriver.LastName,
            Email = newDriver.Email,
            ProfilePicture = "https://i.imgur.com/6VBx3io.png",
            ContactNumber = newDriver.ContactNumber,
        };
        return Ok(new
        {
            message = "Login successful",
            token = jwtToken.Token,
            driver = user
            
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var driver = await _context.Drivers!.FirstOrDefaultAsync(d => d.Email == loginDto.Email);
        if (driver == null)
        {
            return NotFound("Driver with this email does not exist");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, driver.Password))
        {
            return BadRequest("Invalid credentials");
        }

        var jwtToken = await GenerateJwtToken(driver);
        driver.Token = jwtToken.Token;
        await _context.SaveChangesAsync();
        
        var user = new
        {
            Id = driver.DriverId,
            FirstName = driver.FirstName,
            LastName = driver.LastName,
            Email = driver.Email,
            ProfilePicture = "https://i.imgur.com/6VBx3io.png",
            ContactNumber = driver.ContactNumber,
        };
        return Ok(new
        {
            message = "Login successful",
            token = jwtToken.Token,
            driver = user
            
        });
    }


    [HttpPost("add-vehicle")]
    public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var vehicle = _context.Vehicles!.FirstOrDefault(v => v.VehicleNumber == vehicleDto.VehicleNumber);
        if (vehicle != null)
        {
            return BadRequest("Vehicle with this number already exists");
        }
        
        var driver = await _context.Drivers!.FirstOrDefaultAsync(d => d.DriverId == vehicleDto.DriverId);
        if (driver == null)
        {
            return BadRequest("Driver with this id does not exist");
        }
        var newVehicle = new Vehicle
        {
            Driver = driver,
            VehicleNumber = vehicleDto.VehicleNumber,
            VehicleType = vehicleDto.VehicleType,
            VehicleModel = vehicleDto.VehicleModel,
            VehicleAddedAt = DateTime.UtcNow,
            VehicleColor = vehicleDto.VehicleColor,
            AdditionalNotes = vehicleDto.AdditionalNotes,
        };
        await _context.Vehicles!.AddAsync(newVehicle);
        await _context.SaveChangesAsync();
        return Ok(new
        {
            message = "Vehicle added successfully",
        });
    }
}