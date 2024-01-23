using ConfirmationService.BusinessLogic.Models;
using ConfirmationService.BusinessLogic.Services;
using ConfirmationService.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("User")]
[Authorize]
public class UserController(
    UserService userService,
    IHttpContextAccessor httpContextAccessor)
{
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<User> GetUser(long id)
    {
        return (await userService.GetUser(id))!;
    }

    [HttpPut("Register")]
    [AllowAnonymous]
    public async Task<Guid> NewUser(string companyName)
    {
        return await userService.RegisterNewUser(companyName);
    }

    [HttpDelete("DeleteMyself")]
    public async Task Delete()
    {
        await userService.DeleteUser(GetHeaderToken());
    }
    
    [HttpPost("CreateAndSendToClient")]
    public async Task CreateAndSendToClient([FromBody] UserClientModel userClient)
    { 
         await userService.SendConfirmationEmail(userClient, GetHeaderToken());
    }
    
    [HttpGet("GetClients")]
    public async Task<List<ClientModel>> GetClients()
    {
        return await userService.GetUserClients(GetHeaderToken());
    }

    private Guid GetHeaderToken()
    {
        var headers = httpContextAccessor.HttpContext?.Request.Headers;
        var myTokenValue = headers!["MyToken"];
        
        return Guid.Parse(myTokenValue!);
    }
}