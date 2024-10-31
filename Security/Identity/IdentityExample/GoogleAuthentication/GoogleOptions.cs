using IdentityExample.OauthAuthorization;

namespace IdentityExample.GoogleAuthentication;

public class GoogleOptions : ExternalAuthOptions
{
    public override string RedirectPath { get; set; } = "/signin-google";

    public override string AuthenticationUrl => "https://accounts.google.com/o/oauth2/v2/auth";

    public override string ExchangeUrl => "https://www.googleapis.com/oauth2/v4/token";

    public override string DataUrl => "https://www.googleapis.com/oauth2/v2/userinfo";
}