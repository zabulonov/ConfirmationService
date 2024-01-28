using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("SendMail")]
[Authorize]
public class MailSendController(MailSendService mailSendService)
{
    /// <summary>
    /// Sends an email
    /// </summary>
    /// <remarks>
    /// Send an email using MailKit via SMTP.
    /// This request is complete and exists separately from the user and the client. It specifies all the parameters for sending a letter manually, unlike CreateAndSendToClient
    ///
    /// Authorization required Header - MyToken
    /// </remarks>
    /// <param name="emailModel">All parameters are required</param>
    /// <response code="200">OK email sent successfully</response>
    /// <response code="401">Authorization error, check the token in the header</response>
    /// <response code="500">Error connecting to email client. Check that the connection data is correct in appsettings -> MailConnect</response>
    [HttpPost]
    public async Task Send([FromBody] EmailModel emailModel)
    {
        await mailSendService.SendEmail(emailModel);
    }
}