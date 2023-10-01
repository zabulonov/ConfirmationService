using System.Security.Claims;
using System.Text.Encodings.Web;
using ConfirmationService.BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ConfirmationService.Host.Authorization;

public class MyAuthenticationHandler :
    AuthenticationHandler<MyAuthenticationOptions>
{
    public MyAuthenticationHandler(IOptionsMonitor<MyAuthenticationOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        //check header first
        if (!Request.Headers
                .ContainsKey(Options.TokenHeaderName))
        {
            return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
        }

        //get the header and validate
        string token = Request.Headers[Options.TokenHeaderName]!;
        var tokenAsGuid = Guid.Parse(token);

        //usually, this is where you decrypt a token and/or lookup a database.
        var authenticatedUserTokens = UserTokens.GetTokens();

        var authenticatedCompany = authenticatedUserTokens.FirstOrDefault(x => x.Token.Equals(tokenAsGuid));
        if (authenticatedCompany != null)
        {
            //Success! Add details here that identifies the user
            var claims = new List<Claim>
            {
                new("CompanyName", authenticatedCompany.CompanyName)
            };
                
            var claimsIdentity = new ClaimsIdentity
                (claims, Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal
                (claimsIdentity);

            return AuthenticateResult.Success
            (new AuthenticationTicket(claimsPrincipal,
                Scheme.Name));
        }
        
        return AuthenticateResult.Fail($"You've provided incorrect token");
    }
}