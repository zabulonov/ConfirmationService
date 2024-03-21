using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("email-confirmation")]
public class MailСonfirmController(MailConfirmService confirmService)
{
    // плохой код + добавить вариативность не правильной ссылки
    
    /// <summary>
    /// Confirm mail
    /// </summary>
    /// <remarks>
    /// This is a link contained in the letter, when the client clicks on it, the mail is considered confirmed.
    /// 
    /// </remarks>
    /// <param name="token">Your private token from email</param>
    /// <response code="200">OK email confirm successfully</response>
    [HttpGet("confirm")]
    public async Task<ContentResult> Confirmation(Guid token)
    {
        await confirmService.ConfirmMail(token);
        
        String html = "<h1>Your email has been successfully confirmed!</h1>" +
                      "Now you can close the page  <script type='text/javascript'>\n setTimeout(function(){ window.close(); }, 5000);\n </script>";
        
        return new ContentResult
        {
            Content = html,
            ContentType = "text/html"
        };
    }
}