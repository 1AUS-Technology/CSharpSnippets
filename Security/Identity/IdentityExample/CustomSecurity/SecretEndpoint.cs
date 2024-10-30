using Microsoft.AspNetCore.Authorization;

namespace IdentityExample.CustomSecurity;

class SecretEndpoint
{
    [Authorize(Roles = "Administrator")]
    public static async Task Endpoint(HttpContext httpContent)
    {
        await httpContent.Response.WriteAsync("Secret Endpoint");
    }

}