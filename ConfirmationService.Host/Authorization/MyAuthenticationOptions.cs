using Microsoft.AspNetCore.Authentication;

namespace ConfirmationService.Host.Authorization;

public class MyAuthenticationOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "MyAuthenticationScheme";
    public string TokenHeaderName { get; set; } = "MyToken";
}