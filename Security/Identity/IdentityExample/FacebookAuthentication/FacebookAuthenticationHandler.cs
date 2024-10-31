using System.Security.Claims;
using System.Text.Json;
using IdentityExample.OauthAuthorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace IdentityExample.FacebookAuthentication;

public class FacebookOptions : ExternalAuthOptions
{
    public override string RedirectPath { get; set; } = "/signin-facebook";
    public override string Scope { get; set; } = "email";

    public override string AuthenticationUrl =>
        "https://www.facebook.com/v8.0/dialog/oauth";

    public override string ExchangeUrl =>
        "https://graph.facebook.com/v8.0/oauth/access_token";

    public override string DataUrl =>
        "https://graph.facebook.com/v8.0/me?fields=name,email";
}

public class FacebookHandler : ExternalAuthHandler
{
    public FacebookHandler(IOptions<FacebookOptions> options,
        IDataProtectionProvider dp,
        ILogger<FacebookHandler> logger) : base(options, dp, logger)
    {
        string secret = Options.ClientSecret;
        if (string.IsNullOrEmpty(secret) || "MyClientSecret"
                .Equals(secret, StringComparison.OrdinalIgnoreCase))
        {
            logger.LogError("External Authentication Secret Not Set");
        }
    }

    protected override IEnumerable<Claim> GetClaims(JsonDocument jsonDoc)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, jsonDoc.RootElement.GetString("id")));
        claims.Add(new Claim(ClaimTypes.Name,
            jsonDoc.RootElement.GetString("name")?.Replace(" ", "_")));
        claims.Add(new Claim(ClaimTypes.Email,
            jsonDoc.RootElement.GetString("email")));
        return claims;
    }
}