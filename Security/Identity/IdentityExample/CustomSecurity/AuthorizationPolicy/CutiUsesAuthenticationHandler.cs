using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace IdentityExample.CustomSecurity.AuthorizationPolicy;

public class CutiUsesAuthenticationHandler : IAuthenticationHandler
{
    public const string AuthenticationSchemeName = "QueryStringAuthenticationScheme";
    private AuthenticationScheme _scheme;
    private HttpContext _context;

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        _scheme = scheme;
        _context = context;
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        string? user = _context.Request.Cookies[CookiesNames.AuthenticatedUser];

        if (!string.IsNullOrWhiteSpace(user))
        {
            var claim = new Claim(ClaimTypes.Name, user);
            ClaimsIdentity identity = new ClaimsIdentity(_scheme.Name);
            identity.AddClaim(claim);

            var result = AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(identity), _scheme.Name));
            return Task.FromResult(result);
        }

        return Task.FromResult(AuthenticateResult.NoResult());
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        _context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        _context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}