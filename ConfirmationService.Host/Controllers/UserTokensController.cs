using ConfirmationService.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace ConfirmationService.Host.Controllers;

[ApiController]
[Route("user-tokens")]
public class UserTokensController
{
    /// <summary>
    /// FOR TESTS DELETE AT PROD!
    /// </summary>
    /// <remarks>
    /// Returns all users and tokens
    /// 
    /// </remarks>
    /// <response code="200">Return Tokens</response>
    [HttpGet]
    public List<UserTokens.UserToken>? GetTokens()
    {
        return UserTokens.GetTokens();
    }
}