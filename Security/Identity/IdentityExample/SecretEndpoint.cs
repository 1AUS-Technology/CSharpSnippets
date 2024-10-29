class SecretEndpoint
{
    public static async Task Endpoint(HttpContext httpContent)
    {
        await httpContent.Response.WriteAsync("Secret Endpoint");
    }

}
