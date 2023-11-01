using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server.Configurations;
using Server.DbContext;
using server.Helpers;
using Server.Models;
using Server.Models.Dto;
using Server.Utils;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JWTConfig _jwtConfig;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public DriverController(ApplicationDbContext context, IOptions<JWTConfig> jwtConfig,
        TokenValidationParameters tokenValidationParameters)
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
                new
                {
                    message = "Password and confirm password do not match"
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
    
    private async Task<double> getDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // send http request to google maps api
        // get distance
        // return distance
        using (var httpClient = new HttpClient())
        {
            string apiKey = "AIzaSyDFl537nYb0R7zQNcXk9Xrmniy00aM7sK4";
            string apiUrl =
                $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={lat1},{lon1}&destinations={lat2},{lon2}&key={apiKey}";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(jsonResponse);

                string status = data["status"];
                if (status == "OK")
                {
                    double distanceInMeters = (double)data["rows"][0]["elements"][0]["distance"]["value"];
                    // Convert distance from meters to kilometers
                    double distanceInKilometers = distanceInMeters / 1000;
                    return distanceInKilometers;
                }
                else
                {
                    // Handle API error (status is not OK)
                    string errorMessage = data["error_message"];
                    // Handle the error according to your application's requirements
                    throw new Exception($"Google Maps API error: {errorMessage}");
                }
            }
            else
            {
                // Handle HTTP error (response status code is not 200 OK)
                // Handle the error according to your application's requirements
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
            }
        }
    }

    [HttpPost("get-nearest-park")]
    // [Authorize]
    public async Task<IActionResult> GetParkingPlaces([FromBody] GetNearestParkDto getNearestParkDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        double[] maxMinCoordinates = LocationCalculator.CalculateMinMaxCoordinates(getNearestParkDto.DriverLatitude,
            getNearestParkDto.DriverLongitude, getNearestParkDto.Radius);
        var parkingPlaces = await _context.ParkingPlaces!.Where(p =>
            p.Latitude <= maxMinCoordinates[0] && p.Latitude >= maxMinCoordinates[1] &&
            p.Longitude <= maxMinCoordinates[2] && p.Longitude >= maxMinCoordinates[3]).ToListAsync();
        
        // retrieve all slots and group by Category_ID
        foreach (var parkingPlace in parkingPlaces)
        {
            parkingPlaces[parkingPlaces.IndexOf(parkingPlace)].Distance = await getDistance(getNearestParkDto.DriverLatitude,
                getNearestParkDto.DriverLongitude, parkingPlace.Latitude, parkingPlace.Longitude);
        }
        

        List<Slot> slots = new List<Slot>();
        List<Zones> zones = new List<Zones>();
        foreach (var parkingPlace in parkingPlaces)
        {
            var slotsInParkingPlace = await _context.Slots!.Where(s => s.ParkingPlaceId == parkingPlace.ParkingPlaceId).ToListAsync();
            var zonesInParkingPlace = await _context.Zones!.Where(z => z.ParkingPlaceId == parkingPlace.ParkingPlaceId).ToListAsync();
            slots.AddRange(slotsInParkingPlace);
            zones.AddRange(zonesInParkingPlace);
        }
        //get the time difference between arrival and departure time and round it to the nearest 30 minutes
        var timeDifference = getNearestParkDto.DepartureTime - getNearestParkDto.ArrivalTime;
        var timeDifferenceIn30MinuteIntervals = Math.Round(timeDifference.TotalMinutes / 30) * 30;
        
        
        
        return Ok(new
        {
            message = "Parking places retrieved successfully",
            maxMinCoordinates = maxMinCoordinates,
            arrivalTime = getNearestParkDto.ArrivalTime,
            departureTime = getNearestParkDto.DepartureTime,
            parkingPlaces,
            timeDifference= timeDifferenceIn30MinuteIntervals,
        });
    }
    
    //get Zone by id
    [HttpGet("get-zones/{id}")]
    public async Task<IActionResult> GetZones(string id)
    {
        var zones = await _context.Zones!.Where(z => z.ParkingPlaceId == id).ToListAsync();
        return Ok(new
        {
            message = "Zones retrieved successfully",
            zones,
        });
    }
    
    // make reservation for parking slot
    [HttpPost("make-reservation")]
    public async Task<IActionResult> GetParkingPlaces([FromBody] MakeReservationDto makeReservationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var parkingPlace = await _context.ParkingPlaces!.FirstOrDefaultAsync(p =>
            p.ParkingPlaceId == makeReservationDto.ParkingPlaceId);
        if (parkingPlace == null)
        {
            return BadRequest("Parking place with this id does not exist");
        }

        var zone = await _context.Zones!.FirstOrDefaultAsync(z => z.ZoneId == makeReservationDto.ZoneId);
        if (zone == null)
        {
            return BadRequest("Zone with this id does not exist");
        }

        var slot = await _context.Slots!.FirstOrDefaultAsync(s =>
            s.ParkingPlaceId == makeReservationDto.ParkingPlaceId && s.ZoneId == makeReservationDto.ZoneId);
        if (slot == null)
        {
            return BadRequest("Slot with this id does not exist");
        }

        var reservation = new Reservation
        {
            ReservationId = IdGenerator.GenerateId("Res"),
            // convert makeReservationDto.ReservationStartTime to TimeOnly
            ReservationStartAt = TimeOnly.Parse(makeReservationDto.ReservationStartTime.ToString("t")),
            ReservationEndAt = TimeOnly.Parse(makeReservationDto.ReservationEndTime.ToString("t")),
            ParkingPlace = parkingPlace,
            Zone = zone,
            Slot = slot,
            ReservedAt = DateTime.UtcNow,
            PaymentMethod = makeReservationDto.PaymentMethod,
            PaymentStatus = "Success",
            ZoneId = null,
            SlotId = null,
            TotalPayment = makeReservationDto.TotalPayment,
            ReservationDate = default,
            ParkedAt = default,
            ExitedAt = default,
            ReservationStatus = "Reserved",
            ReservationType = "Online",
            CancelledAt = default,
            CancellationReason = "",
            ParkingPlaceId = null,
        };
        
        //make slot unavailable
        var slotToUpdate = await _context.Slots!.FirstOrDefaultAsync(s =>
            s.ParkingPlaceId == makeReservationDto.ParkingPlaceId && s.ZoneId == makeReservationDto.ZoneId);
        if (slotToUpdate == null)
        {
            return BadRequest("Slot with this id does not exist");
        }
        slotToUpdate.SlotStatus = "Parked";
        _context.Slots!.Update(slotToUpdate);
        
        
        await _context.Reservations!.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return Ok(new
        {
            message = "Reservation made successfully",
        });
        
    }
    
    
}