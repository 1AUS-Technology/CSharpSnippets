namespace IdentityExample.CustomSecurity;

public class CustomAuthorization(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        if (context.GetEndpoint()?.DisplayName == "secret")
        {
            if (context.User.Identity.IsAuthenticated)
            {
                if (context.User.IsInRole("Administrator"))
                {
                    await next(context);
                }
                else
                {
                    SetForbidResponseCode(context);
                }
            }
            else
            {
                SetChallengeResponseCode(context);
            }
        }
        else
        {
            await next(context);
        }
    }

    private void SetChallengeResponseCode(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private void SetForbidResponseCode(HttpContext context)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
    }
}