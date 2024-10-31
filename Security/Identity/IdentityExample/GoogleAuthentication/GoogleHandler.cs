using System.Security.Claims;
using System.Text.Json;
using IdentityExample.OauthAuthorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace IdentityExample.GoogleAuthentication;

public class GoogleHandler : ExternalAuthHandler
{
    public GoogleHandler(IOptions<GoogleOptions> options,
        IDataProtectionProvider dp,
        ILogger<GoogleHandler> logger) : base(options, dp, logger)
    {
    }

    protected override IEnumerable<Claim> GetClaims(JsonDocument jsonDoc)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, jsonDoc.RootElement.GetString("id")),

            new(ClaimTypes.Name, jsonDoc.RootElement.GetString("name")?.Replace(" ", "_")),

            new(ClaimTypes.Email, jsonDoc.RootElement.GetString("email"))
        ];
        return claims;
    }

    protected override async Task<string> GetAuthenticationUrl(AuthenticationProperties properties)
    {
        if (CheckCredentials())
        {
            return await base.GetAuthenticationUrl(properties);
        }

        return string.Format(Options.ErrorUrlTemplate, ErrorMessage);
    }

    private bool CheckCredentials()
    {
        string secret = Options.ClientSecret;
        string id = Options.ClientId;
        string defaultVal = "ReplaceMe";
        if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(id) || defaultVal.Equals(secret) || defaultVal.Equals(secret))
        {
            ErrorMessage = "External Authentication Secret or ID Not Set";
            _logger.LogError("External Authentication Secret or ID Not Set");
            return false;
        }

        return true;
    }
}