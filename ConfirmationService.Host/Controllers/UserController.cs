using ConfirmationService.BusinessLogic;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(UserService userService, MailSendService mailSendService, SendConfirmationService sendConfirmationService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _mailSendService = mailSendService;
        _sendConfirmationService = sendConfirmationService;
        _httpContextAccessor = httpContextAccessor;
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
    public async Task Delete()
    {
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;
        string myTokenValue = headers["MyToken"];
        await _userService.DeleteUser(Guid.Parse(myTokenValue));
    }
    
    [HttpPost("CreateAndSendToClient")]
    public async Task CreateAndSendToClient([FromBody] UserClientModel userClient)
    { 
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;
        string myTokenValue = headers["MyToken"];
        
         //var confirmToken = await _sendConfirmationService.CreateUserOfClient(userClient, Guid.Parse(myTokenValue));
         await _userService.SendConfirmationEmail(userClient, Guid.Parse(myTokenValue));
    }
    
    [HttpGet("GetClients")]
    public async Task<List<ClientModel>> GetClients()
    {
        var headers = _httpContextAccessor.HttpContext?.Request.Headers;
        string myTokenValue = headers["MyToken"];
        
        return await _userService.GetUserClients(Guid.Parse(myTokenValue));
    }
    
}