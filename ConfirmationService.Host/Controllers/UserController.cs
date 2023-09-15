using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("User")]
public class UserController
{
    private readonly UserService _userService;
    private readonly MailSendService _mailSendService;
    private readonly SendConfirmationService _sendConfirmationService;

    public UserController(UserService userService, MailSendService mailSendService, SendConfirmationService sendConfirmationService)
    {
        _userService = userService;
        _mailSendService = mailSendService;
        _sendConfirmationService = sendConfirmationService;
    }
    
    [HttpGet("{id}")]
    public async Task<User> GetUser(long id)
    {
        return (await _userService.GetUser(id))!;
    }

    [HttpPut("Register")]
    public async Task<Guid> NewUser(string companyName)
    {
        return await _userService.RegisterNewUser(companyName);
    }

    [HttpDelete("DeleteMyself")]
    public async Task Delete([FromBody] Guid token)
    {
        await _userService.DeleteUser(token);
    }
    
    [HttpPost("CreateAndSendToClient")]
    public async Task CreateAndSendToClient([FromBody] ClientOfUserModel client)
    {
        var token = await _sendConfirmationService.CreateUserOfClient(client);
        await _mailSendService.SendEmailToClient(client, token);
    }
    
    [HttpGet("GetClients")]
    public async Task<List<ClientOfUser>> GetClients(Guid token)
    {
        var id = _userService.TokenToPK(token);
        return _userService.GetUserClients(id).Result;
    }
    
}