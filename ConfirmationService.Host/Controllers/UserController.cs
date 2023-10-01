using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

/*
 * 1. зарегать нового юзера
 * 2. добавить юзеру клиента
 * 3. отправить confirmation email для клиента юзера
 * 4. обработать переход по подтверждающей ссылке
 * 5. получить информацию о своих юзере(ах) 
 */

[ApiController]
[Route("User")]
[Authorize]
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
    [AllowAnonymous]
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
    public async Task CreateAndSendToClient([FromBody] UserClientModel userClient)
    { 
        await _sendConfirmationService.CreateUserOfClient(userClient);
        // await _mailSendService.SendEmailToClient(client, token);
    }
    
    [HttpGet("GetClients")]
    public async Task<List<ClientModel>> GetClients(Guid token)
    {
        return await _userService.GetUserClients(token);
    }
    
}