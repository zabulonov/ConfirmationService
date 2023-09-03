using ConfirmationService.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("email-confirmation")]
public class MailСonfirmController
{
    [HttpGet("confirm")]
    public string Confirmation(int id)
    {
        return $"email confirmed!";
    }
    
    [HttpPost("new")]
    public void NewConfirmation(ConfirmEmailModel model)
    {
        
    }
}