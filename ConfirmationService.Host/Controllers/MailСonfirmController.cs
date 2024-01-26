using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("email-confirmation")]
public class MailСonfirmController(MailConfirmService confirmService)
{
    // плохой код + добавить вариативность не правильной ссылки
    [HttpGet("confirm")]
    public async Task<ContentResult> Confirmation(Guid token)
    {
        await confirmService.ConfirmMail(token);
        
        String html = "<h1>Your email has been successfully confirmed!</h1>" +
                      "Now you can close the page";
        return new ContentResult
        {
            Content = html,
            ContentType = "text/html"
        };
    }
}