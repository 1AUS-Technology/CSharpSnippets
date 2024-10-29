namespace IdentityExample.CustomSecurity;

public class CustomSignInAndSignOut
{
    public static async Task SignIn(HttpContext context)
    {
        string? user = context.Request.Query["user"];
        if (user != null)
        {
            context.Response.Cookies.Append("authenticatedUser", user);
            await context.Response.WriteAsync($"Authenticated user {user}");
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    public static async Task SignOut(HttpContext context)
    {
        context.Response.Cookies.Delete("authenticatedUser");
        await context.Response.WriteAsync("Signed Out Cu ti");
    }
}