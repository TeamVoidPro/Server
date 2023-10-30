using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DbContext;
using Server.Models;
using Server.Models.Dto;
using System.Net;
using System.Net.Mail;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VerifierController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ParkingPlaceOwnerDto>> GetOwners()
        {
            var owner = _context.ParkingPlaceOwners.Where(p => p.acceptStatus == false);
            return Ok(owner);
        }

        [HttpGet("id")]
        public ActionResult<IEnumerable<ParkingPlaceOwnerDto>> GetOwnerById(string id)
        {
            var owner = _context.ParkingPlaceOwners.FirstOrDefault(p => p.OwnerId == id && p.acceptStatus == false);
            return Ok(owner);
        }

        [HttpPost("On-site-verification")]
        public ActionResult<OnSiteVerificationsDto> SiteVerification([FromBody] OnSiteVerificationsDto verificationDto)
        {
            if (verificationDto == null)
            {
                return BadRequest(verificationDto);
            }
            OnSiteVerifications model = new()
            {
                OwnerId = verificationDto.OwnerId,
                EasyAccess = verificationDto.EasyAccess,
                SlotCount = verificationDto.SlotCount,
                SlotDimensions = verificationDto.SlotDimensions,
                LandDimensions = verificationDto.LandDimensions,
                DocumentPresentation = verificationDto.DocumentPresentation,
                NoDocumentReason = verificationDto.NoDocumentReason,
                SerialMatch = verificationDto.SerialMatch,
                NoSerialMatchReason = verificationDto.NoSerialMatchReason,
                Concerns = verificationDto.Concerns,
                SuitablePlace = verificationDto.SuitablePlace,
                NotSuitableReasons = verificationDto.NotSuitableReasons,
                DisasterProne = verificationDto.DisasterProne,
                DisasterReasons = verificationDto.DisasterReasons,
                GeneralImpression = verificationDto.GeneralImpression,
                Images = verificationDto.Images
            };

            _context.OnsiteVerifications.Add(model);
            _context.SaveChanges();

            return Ok("Success!");
        }

        [HttpPost("verification-schedule")]
        public async Task<IActionResult> ScheduleSiteVerfication([FromBody] VerificationScheduleDto verifyDto)
        {
            var check = _context.ParkingPlaceOwners!.FirstOrDefault(p => p.OwnerId == verifyDto.OwnerId);

            if (check == null)
            {
                return BadRequest("User does not exist");
            }

            var FirstName = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.FirstName).FirstOrDefault();
            var LastName = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.LastName).FirstOrDefault();
            var LandAddressNumber = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.LandAddressNumber).FirstOrDefault();
            var LandAddressStreet = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.LandAddressStreet).FirstOrDefault();
            var LandAddressCity = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.LandAddressCity).FirstOrDefault();
            var LandAddressProvince = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.LandAddressProvince).FirstOrDefault();
            var Email = _context.ParkingPlaceOwners.Where(p => p.OwnerId == verifyDto.OwnerId).Select(p => p.Email).FirstOrDefault();

            VerificationSchedule model = new()
            {
                OwnerId = verifyDto.OwnerId,
                OwnerName = FirstName + LastName,
                LandAddress = LandAddressNumber + "" + LandAddressStreet + "" + LandAddressCity + "" + LandAddressProvince,
                Date = verifyDto.Date,
                Time = verifyDto.Time,
                EmployeeId = verifyDto.EmployeeId,
            };
            _context.VerificationSchedule.Add(model);
            _context.SaveChanges();

            var fromMail = "parkease50@gmail.com";
            var fromPassword = "udseompxmhreprdb";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "On-Site Inspection Date";
            message.To.Add(new MailAddress(Email));
            message.Body = $"<html><body><p>Hello {FirstName},</p><p>One of our Verifiers will be at your plot of land on: {verifyDto.Date} at {verifyDto.Time}. Please make sure to bring all your legal documents. Failure to do so will result rejection of parking place.</p><p>Best regards,<br>Park Ease Team</p></body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);

            return Ok("Success!");
  
        }
        

        [HttpPut("approve-park")]
        public async Task<IActionResult> ApprovePark(string id) 
        {
            if (id == null)
            {
                return BadRequest("No valid parameters");
            }

            _context.ParkingPlaceOwners
                .Where(p => p.OwnerId == id)
                .ExecuteUpdate(b =>
                    b.SetProperty(p => p.acceptStatus, true)
                );

            _context.VerificationSchedule
                .Where(p => p.OwnerId == id)
                .ExecuteDelete();

            var owner = _context.ParkingPlaceOwners.FirstOrDefault(p => p.OwnerId == id);
            if (owner == null)
            {
                return BadRequest("No Owner");
            }

            var fromMail = "parkease50@gmail.com";
            var fromPassword = "udseompxmhreprdb";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Approval of Parking Place";
            message.To.Add(new MailAddress(owner.Email));
            message.Body = "<html><body><p>Hello,</p><p>Your plot of land has been approved as a parking place on our platform! </p><p>Best regards,<br>Park Ease Team</p></body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential (fromMail, fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);

            return Ok("Email Sent");

        }

        [HttpGet("pending-onsite-verifications")]
        public ActionResult<IEnumerable<VerificationScheduleDto>> OnsitePendingVerifications()
        {
            return Ok(_context.VerificationSchedule.ToList());
        }


    }
}
