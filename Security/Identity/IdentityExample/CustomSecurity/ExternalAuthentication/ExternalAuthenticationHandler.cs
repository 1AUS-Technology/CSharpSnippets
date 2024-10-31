using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.ExternalAuthentication;

public class ExternalAuthenticationHandler : IAuthenticationHandler
{
    public AuthenticationScheme Scheme { get; set; }
    public HttpContext Context { get; set; }
    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        Context = context;
        Scheme = scheme;
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.NoResult());
    }

    public async Task ChallengeAsync(AuthenticationProperties? properties)
    {
        // hard-cde - this should be user detail
        ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
        identity.AddClaims([
            new Claim(ClaimTypes.NameIdentifier, "SomeUniqueID"),
            new Claim(ClaimTypes.Email, "alice@example.com"),
            new Claim(ClaimTypes.Name, "Alice")
        ]);

        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
        await Context.SignInAsync(IdentityConstants.ExternalScheme, principal, properties);

        Context.Response.Redirect(properties.RedirectUri);
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }
}