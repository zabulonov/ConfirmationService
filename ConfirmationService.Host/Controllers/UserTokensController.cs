using ConfirmationService.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("user-tokens")]
public class UserTokensController
{
    [HttpGet]
    public List<UserTokens.UserToken>? GetTokens()
    {
        return UserTokens.GetTokens();
    }
}