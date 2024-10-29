using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace IdentityExample.CustomSecurity.AuthorizationPolicy;

public class AuthenticationSignInHandler : IAuthenticationSignInHandler
{
    private HttpContext context;
    private AuthenticationScheme scheme;

    public Task InitializeAsync(AuthenticationScheme authScheme,
        HttpContext httpContext)
    {
        context = httpContext;
        scheme = authScheme;
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.NoResult());
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        context.Response.Redirect("/signin/401");
        return Task.CompletedTask;
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        context.Response.Redirect("/signout/403");
        return Task.CompletedTask;
    }

    public Task SignOutAsync(AuthenticationProperties? properties)
    {
        context.Response.Cookies.Delete(CookiesNames.AuthenticatedUser);
        return Task.CompletedTask;
    }

    public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
    {
        context.Response.Cookies.Append(CookiesNames.AuthenticatedUser, user.Identity.Name);
        return Task.CompletedTask;
    }
}