using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QrCodeController : ControllerBase
{
    [HttpGet("generate-qr-code")]
    public Task<byte[]> GenerateQrCode(string text)
    {
        byte[] QrCode = Array.Empty<byte>();

        if (!string.IsNullOrEmpty(text))
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            QrCode = qrCode.GetGraphic(20);
            
        }
        
        return Task.FromResult(QrCode);
    }
    
    [HttpGet("generate-qr-code-image")]
    public  IActionResult GenerateQrCodeImage(string text)
    {
        byte[] QrCode = Array.Empty<byte>();

        if (!string.IsNullOrEmpty(text))
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            QrCode = qrCode.GetGraphic(20);
            
        }
        
        return File(QrCode, "image/png");
    }
}