using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using Server.DbContext;
using Server.Models.Dto;

namespace Server.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    
    public EmailController(
        ISendGridClient sendGridClient,
        IConfiguration configuration,
        ApplicationDbContext context)
    {
        _sendGridClient = sendGridClient;
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("verify-account-email")]
    public async Task<IActionResult> VerifyAccountEmail([FromBody] VerifyAccountDto verifyAccountDto)
    {
        var fromEmail = _configuration.GetSection("EmailServer")!.GetSection("FromEmail")!.Value;
        var fromName = _configuration.GetSection("EmailServer")!.GetSection("FromName")!.Value;
        
        var employee = _context.Employees!.FirstOrDefault(e => e.EmployeeId == verifyAccountDto.EmployeeId);
        
        if (employee == null)
        {
            return BadRequest(new
            {
                status = "Employee not found",
            });
        }
        
        
        string htmlContent = $@"<html>
                                        <body>
                                            <p>Hello,</p>
                                            <p>Your account has been created. Your verification code is: {verifyAccountDto.VerificationCode}</p>
                                            <p>Please click the following link to complete your registration: <a href=""http://127.0.0.1:3000/verify-account/{verifyAccountDto.EmployeeId}"">http://127.0.0.1:3000/verify-account/{verifyAccountDto.EmployeeId}</a></p>
                                            <p>Best regards,<br>Park Ease Team</p>
                                        </body>
                                    </html>";
        
        var message = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Verify your account",
            HtmlContent = htmlContent
        };
        
        message.AddTo(employee.Email);
        
        var response = await _sendGridClient.SendEmailAsync(message);
        
        var status = response.IsSuccessStatusCode ? "Email Send Successfully" : "Email Send Failed";
        
        return Ok(new
        {
            status,
        });
    }
}