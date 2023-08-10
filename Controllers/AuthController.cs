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
    private readonly TokenValidationParameters _tokenValidationParameters;


    public AuthController(ApplicationDbContext context, IOptions<JWTConfig> jwtConfig, TokenValidationParameters tokenValidationParameters)
    {
        _context = context;
        _jwtConfig = jwtConfig.Value;
        _tokenValidationParameters = tokenValidationParameters;
    }
    

    [HttpPost("employee-register")]
    public async Task<IActionResult> Register([FromBody] EmployeeDto employeeDto)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = await _context.Employees!.FirstOrDefaultAsync(e => e.Email == employeeDto.Email);
        if (employee != null)
        {
            ModelState.AddModelError("Email", "Email already exists");
            return BadRequest(ModelState);
        }
        

        var newEmployee = new Employee
        {
            EmployeeId = IdGenerator.GenerateId("EMP"),
            FirstName = employeeDto.FirstName,
            LastName = employeeDto.LastName,
            Email = employeeDto.Email,
            Password = "",
            AddressLine1 = employeeDto.AddressLine1,
            Street = employeeDto.Street,
            City = employeeDto.City,
            ContactNumber = employeeDto.ContactNumber,
            Nic = employeeDto.Nic,
            Role = employeeDto.Role,
            ProfilePicture = "https://i.imgur.com/6VBx3io.png",
            Token = "",
            AccountCreatedAt = DateTime.UtcNow,
            IsVerified = false
        };

        var random = new Random();
        var code = random.Next(100000, 999999).ToString();

        var newVerification = new VerificationCodes
        {
            Id = IdGenerator.GenerateRandomId(20),
            Code = BCrypt.Net.BCrypt.HashPassword(code),
            IsUsed = false,
            UserId = newEmployee.EmployeeId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
        
        await _context.VerificationCodes!.AddAsync(newVerification);
        await _context.Employees!.AddAsync(newEmployee);
        await _context.SaveChangesAsync();
        
        return Ok(new
        {
            message = "Successfully registered",
            verificationCode = code,
            employeeId = newEmployee.EmployeeId
        });
    }
    
    [HttpPost("employee-login")]
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
        
        if(employee.IsActivated == false)
        {
            return BadRequest(new {message = "Account is not activated"});
        }
        
        if(employee.IsVerified == false)
        {
            return BadRequest(new {message = "Account is not verified"});
        }

        var jwtTokens = await GenerateJwtToken(employee);
        employee.Token = jwtTokens.Token;
        await _context.SaveChangesAsync();

        var user = new
        {
            Id = employee.EmployeeId,
            Firstname = employee.FirstName,
            Lastname = employee.LastName,
            Email = employee.Email,
            Role = employee.Role,
            ProfilePicture = employee.ProfilePicture,
        };
        
        
        return Ok( new
        {
            message = "Successfully logged in",
            tokens = jwtTokens,
            employee = user
        });
    }
    
    [HttpPost("employee-logout")]
    public async Task<IActionResult> Logout(LogoutDto logoutDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = await _context.Employees!.FirstOrDefaultAsync(e => e.EmployeeId == logoutDto.EmployeeId);
        if (employee == null)
        {
            return BadRequest(new {message = "Invalid credentials"});
        }

        var refreshToken = await _context.RefreshTokens!.FirstOrDefaultAsync(e => e.EmployeeId == logoutDto.EmployeeId);
        refreshToken!.IsRevoked = true;
        _context.RefreshTokens!.Update(refreshToken);
        await _context.SaveChangesAsync();

        employee.Token = "";
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Successfully logged out"
        });
    }

    private async Task<AuthResults> GenerateJwtToken(Employee employee)
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
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenResult = tokenHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            Id = IdGenerator.GenerateRandomId(60),
            EmployeeId = employee.EmployeeId,
            Token = RandomString(20) + Guid.NewGuid(),
            Expires = DateTime.UtcNow.AddMonths(6),
            IsUsed = false,
            IsRevoked = false,
            JwtId = token.Id
        };
        
        await _context.RefreshTokens!.AddAsync(refreshToken);
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
    
    [HttpPost("employee-refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        if (ModelState.IsValid)
        {
            var result = await VerifyAndGenerateToken(refreshTokenDto);
            
            if(result == null)
                return BadRequest(new AuthResults()
                {
                    Errors = new[]
                    {
                        "Invalid Tokens"
                    },
                    Result = false
                });
            
            return Ok(result);
        }

        return BadRequest(new AuthResults()
        {
            Errors = new[]
            {
                "Invalid Parameters"
            },
            Result = false
        });
    }

    private async Task<AuthResults?> VerifyAndGenerateToken(RefreshTokenDto refreshTokenDto)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            _tokenValidationParameters.ValidateLifetime = false;
            
            var tokenInVerification = jwtTokenHandler.ValidateToken(refreshTokenDto.Token, _tokenValidationParameters, out var validatedToken);
            
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                    return null;
            }
            
            var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value ?? string.Empty);
            
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
            
            if (expiryDate > DateTime.UtcNow)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This token hasn't expired yet"}
                };
            
            var storedRefreshToken = await _context.RefreshTokens!.FirstOrDefaultAsync(x => x.Token == refreshTokenDto.RefreshToken);
            
            if (storedRefreshToken == null)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This refresh token does not exist"}
                };

            if (storedRefreshToken.IsUsed)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This refresh token has been used"}
                };

            if (storedRefreshToken.IsRevoked)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This refresh token has been revoked"}
                };
            
            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            
            if (storedRefreshToken.JwtId != jti)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This refresh token has been invalidated"}
                };
            
            if(storedRefreshToken.Expires < DateTime.UtcNow)
                return new AuthResults()
                {
                    Result = false,
                    Errors = new[] {"This refresh token has expired"}
                };
            
            storedRefreshToken.IsUsed = true;
            _context.RefreshTokens!.Update(storedRefreshToken);
            await _context.SaveChangesAsync();
            
            var user = await _context.Employees!.FirstOrDefaultAsync(x => x.EmployeeId == storedRefreshToken.EmployeeId);

            if (user != null)
            {
                var tokens = await GenerateJwtToken(user);
                
                user.Token = tokens.Token;
                _context.Employees!.Update(user);
                await _context.SaveChangesAsync();
                
                return tokens; 
            }

            return new AuthResults()
            {
                Result = false,
                Errors = new[] {"Something went wrong"}
            };
        }
        catch (Exception)
        {
            return new AuthResults()
            {
                Result = false,
                Errors = new[] { "Server Error" }
            };
        }
    }

    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTimeVal;
    }

    [HttpPost("verify-account")] 
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountDto verifyAccountDto)
    {
        var employee = _context.Employees!.FirstOrDefault(x => x.EmployeeId == verifyAccountDto.EmployeeId);
        
        if (employee == null)
            return BadRequest(new
            {
                VerificationCode = "Invalid request"
            });
        
        var verificationRecord = _context.VerificationCodes!.FirstOrDefault(x => x.UserId == employee.EmployeeId);
        
        if (verificationRecord == null)
            return BadRequest(new
            {
                VerificationCode = "Invalid request"
            });
        
        if(verificationRecord.IsUsed)
            return BadRequest(new
            {
                VerificationCode = "Verification code already used"
            });
        
        if(!BCrypt.Net.BCrypt.Verify(verifyAccountDto.VerificationCode, verificationRecord.Code))
            return BadRequest(new
            {
                VerificationCode = "Invalid verification code"
            });
        
        if(verificationRecord.ExpiresAt < DateTime.UtcNow)
            return BadRequest(new
            {
                VerificationCode = "Verification code expired"
            });
        
        if(employee.IsVerified)
            return BadRequest(new
            {
                VerificationCode = "Account already verified"
            });
        
        verificationRecord.IsUsed = true;
        _context.VerificationCodes!.Update(verificationRecord);
        employee.IsVerified = true;
        _context.Employees!.Update(employee);
        await _context.SaveChangesAsync();
        
        return Ok(new AuthResults()
        {
            Result = true,
            Errors = null
        });
    }

    [HttpPost("create-password")]
    public async Task<IActionResult> CreatePassword([FromBody] CreatePasswordDto createPasswordDto)
    {
        var employee = _context.Employees!.FirstOrDefault(x => x.EmployeeId == createPasswordDto.EmployeeId);

        if (employee == null)
            return BadRequest(new AuthResults()
            {
                Errors = new[]
                {
                    "Invalid Request"
                },
                Result = false
            });
        
        if(employee.Password != "")
            return BadRequest(new AuthResults()
            {
                Errors = new[]
                {
                    "Password already set for the user"
                },
                Result = false
            });
        
        if(employee.IsVerified == false)
            return BadRequest(new AuthResults()
            {
                Errors = new[]
                {
                    "Email not verified"
                },
                Result = false
            });
        

        if (createPasswordDto.Password != createPasswordDto.ConfirmPassword)
        {
            return BadRequest(new AuthResults()
            {
                Errors = new[]
                {
                    "Passwords do not match"
                },
                Result = false
            });
        }
        
        if (employee.IsActivated == false)
        {
            employee.IsActivated = true;
            employee.Password = BCrypt.Net.BCrypt.HashPassword(createPasswordDto.Password);
            _context.Employees!.Update(employee);
            await _context.SaveChangesAsync();
            
            return Ok(new AuthResults()
            {
                Result = true,
                Errors = null
            });
        }
        
        return BadRequest(new AuthResults()
            {
                Result = false,
                Errors = new[] { "Account is already activated." }
            });
        
    }

}