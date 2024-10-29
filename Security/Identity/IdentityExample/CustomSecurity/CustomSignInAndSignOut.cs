using System.Security.Claims;
using IdentityExample.CustomSecurity.AuthorizationPolicy;
using Microsoft.AspNetCore.Authentication;

namespace IdentityExample.CustomSecurity;

public class CustomSignInAndSignOut
{
    public static async Task SignIn(HttpContext context)
    {
        string? user = context.Request.Query["user"];
        if (user != null)
        {
            var claim = new Claim(ClaimTypes.Name, user);
            ClaimsIdentity queryStringIdentity = new ClaimsIdentity(CutiUsesAuthenticationHandler.AuthenticationSchemeName);
            queryStringIdentity.AddClaim(claim);

            // ask the framework to sign in the user using the claim provided
            // this will trigger the authentication signin and sign out handler
            await context.SignInAsync(new ClaimsPrincipal(queryStringIdentity));
            
            await context.Response.WriteAsync($"Authenticated user {user}");
        }
        else
        {
            // Send 401 back to clients
            await context.ChallengeAsync();
            //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    public static async Task SignOut(HttpContext context)
    {
        //context.Response.Cookies.Delete("authenticatedUser");
        // we can ask the framework to sign out user using the sign in & out handler
        await context.SignOutAsync(CutiUsesAuthenticationHandler.AuthenticationSchemeName);

        await context.Response.WriteAsync("Signed Out Cu ti");
    }
}