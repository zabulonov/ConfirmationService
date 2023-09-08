using ConfirmationService.BusinessLogic.Services;
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

    [HttpPut("Register")]
    public async Task<Guid> NewUser([FromBody] string companyName)
    {
        return _userService.RegisterNewUser(companyName);
    }

    [HttpDelete("DeleteMyself")]
    public void Delete([FromBody] Guid token)
    {
        _userService.DeleteUser(_userService.CheckToken(token));
    }
    
    
}