using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("SendMail")]
public class MailSendController(MailSendService mailSendService)
{
    /// <summary>
    /// Description here
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///    GET api/v1/blogs
    ///
    /// </remarks>
    /// <param name="filter">Description here</param>
    /// <returns>Description here</returns>
    /// <response code="200">Description here</response>
    /// <response code="400">Description here</response>
    [HttpPost]
    public async Task Send([FromBody] EmailModel emailModel)
    {
        await mailSendService.SendEmail(emailModel);
    }
}