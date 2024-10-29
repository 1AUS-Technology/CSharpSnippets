using System.Security.Claims;

class CustomAuthentication
{
    private RequestDelegate _next;

    public CustomAuthentication(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //string? user = context.Request.Query["user"];
        // instead of getting the user detail from the query string, we get it from the cookies
        string? user = context.Request.Cookies["authenticatedUser"];
        if (!string.IsNullOrWhiteSpace(user))
        {
            Claim userClaim = new Claim(ClaimTypes.Name, user);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { userClaim }, "Custom");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            context.User = claimsPrincipal;
        }
        await _next(context);
    }
}
