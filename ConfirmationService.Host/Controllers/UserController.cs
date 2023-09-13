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

    public UserController(UserService userService)
    {
        _userService = userService;
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
}