using Microsoft.AspNetCore.Mvc;

namespace FeroCourse.ServicesInterface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }

}

///Mail sending example method
///public async Task<IActionResult> SendTestEmail()
///{
///    await _emailService.SendEmailAsync("user@example.com", "Test Email", "<h1>Hello!</h1>");
///    return Content("Email Sent Successfully");
///}