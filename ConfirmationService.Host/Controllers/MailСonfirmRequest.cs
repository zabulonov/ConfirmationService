using ConfirmationService.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("EmailConfirmation")]
public class MailСonfirmRequest
{
    [HttpGet("Confirm")]
    public string Confirmation(int id)
    {
        return $"email confirmed!";
    }
    
    [HttpPost("new")]
    public async void NewConfirnation(ConfirmEmailModel model)
    {
        
    }
}