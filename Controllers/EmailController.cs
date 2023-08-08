using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Server.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ISendGridClient _sendGridClient;
    private readonly IConfiguration _configuration;
    
    public EmailController(
        ISendGridClient sendGridClient,
        IConfiguration configuration)
    {
        _sendGridClient = sendGridClient;
        _configuration = configuration;
    }

    [HttpGet("send-text-email")]
    public async Task<IActionResult> SendTextEmail(string toEmail)
    {
        var fromEmail = _configuration.GetSection("EmailServer")!.GetSection("FromEmail")!.Value;
        var fromName = _configuration.GetSection("EmailServer")!.GetSection("FromName")!.Value;

        var message = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = "Plain Text Email",
            PlainTextContent = "Hello, Email!",
        };
        
        message.AddTo(toEmail);
        
        var response = await _sendGridClient.SendEmailAsync(message);
        
        var status = response.IsSuccessStatusCode ? "Email Send Successfully" : "Email Send Failed";
        
        return Ok(new
        {
            status
        });
    }
}