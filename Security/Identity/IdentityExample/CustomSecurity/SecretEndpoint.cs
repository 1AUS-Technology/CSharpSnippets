using Microsoft.AspNetCore.Authorization;

class SecretEndpoint
{
    [Authorize(Roles = "Administrator")]
    public static async Task Endpoint(HttpContext httpContent)
    {
        await httpContent.Response.WriteAsync("Secret Endpoint");
    }

}
