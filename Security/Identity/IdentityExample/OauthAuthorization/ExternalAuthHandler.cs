using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityExample.OauthAuthorization;

public class ExternalAuthOptions
{
    public string ClientId { get; set; } = "MyClientID";
    public string ClientSecret { get; set; } = "MyClientSecret";

    public virtual string RedirectRoot { get; set; } = "http://localhost:5000";
    public virtual string RedirectPath { get; set; } = "/signin-external";
    public virtual string Scope { get; set; } = "openid email profile";
    public virtual string StateHashSecret { get; set; } = "mysecret";

    public virtual string AuthenticationUrl { get; set; } = "http://localhost:5000/DemoExternalAuth/authenticate";

    public virtual string ExchangeUrl { get; set; }
        = "http://localhost:5000/DemoExternalAuth/exchange";

    public virtual string ErrorUrlTemplate { get; set; } = "/externalsignin?error={0}";

    public virtual string DataUrl { get; set; }
        = "http://localhost:5000/DemoExternalAuth/data";
}

public class ExternalAuthHandler : IAuthenticationHandler
{
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly ILogger<ExternalAuthHandler> _logger;

    public ExternalAuthHandler(IOptions<ExternalAuthOptions> optionMonitor, IDataProtectionProvider dataProtectionProvider,
        ILogger<ExternalAuthHandler> logger)
    {
        _dataProtectionProvider = dataProtectionProvider;
        _logger = logger;
        Options = optionMonitor.Value;
    }

    public ExternalAuthOptions Options { get; set; }
    public PropertiesDataFormat PropertiesFormatter { get; set; }

    public HttpContext Context { get; set; }

    public AuthenticationScheme Scheme { get; set; }
    public string ErrorMessage { get; set; }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        Scheme = scheme;
        Context = context;
        PropertiesFormatter = new PropertiesDataFormat(_dataProtectionProvider.CreateProtector(typeof(ExternalAuthOptions).FullName));
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        return Task.FromResult(AuthenticateResult.NoResult());
    }

    public async Task ChallengeAsync(AuthenticationProperties? properties)
    {
        Context.Response.Redirect(await GetAuthenticationUrl(properties));
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }

    public virtual async Task<bool> HandleRequestAsync()
    {
        if (Context.Request.Path.Equals(Options.RedirectPath))
        {
            string authCode = await GetAuthenticationCode();
            (string token, string state) = await GetAccessToken(authCode);

            // process token
            if (!string.IsNullOrEmpty(token))
            {
                IEnumerable<Claim> claims = await GetUserData(token);
                if (claims != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
                    identity.AddClaims(claims);
                    ClaimsPrincipal claimsPrincipal
                        = new ClaimsPrincipal(identity);
                    AuthenticationProperties authenticationProperties = PropertiesFormatter.Unprotect(state);
                    
                    authenticationProperties.StoreTokens(new AuthenticationToken[]{ new AuthenticationToken()
                    {
                        Name = "access_token",
                        Value = token
                    }});

                    await Context.SignInAsync(IdentityConstants.ExternalScheme, claimsPrincipal, authenticationProperties);
                    Context.Response.Redirect(authenticationProperties.RedirectUri);
                    return true;
                }

                Context.Response.Redirect(string.Format(Options.ErrorUrlTemplate,
                    ErrorMessage));
                return true;
            }
        }

        return false;
    }

    protected virtual async Task<IEnumerable<Claim>> GetUserData(string accessToken)
    {
        HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, Options.DataUrl);
        msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        HttpResponseMessage response = await new HttpClient().SendAsync(msg);
        string jsonData = await response.Content.ReadAsStringAsync();

        JsonDocument jsonDoc = JsonDocument.Parse(jsonData);
        var error = jsonDoc.RootElement.GetString("error");
        if (error != null)
        {
            ErrorMessage = "User Data Error";
            _logger.LogError(ErrorMessage);
            _logger.LogError(jsonData);
            return null;
        }

        return GetClaims(jsonDoc);
    }

    protected virtual IEnumerable<Claim> GetClaims(JsonDocument jsonDoc)
    {
        List<Claim> claims = new List<Claim>();

        claims.Add(new Claim(ClaimTypes.NameIdentifier,
            jsonDoc.RootElement.GetString("id")));
        claims.Add(new Claim(ClaimTypes.Name,
            jsonDoc.RootElement.GetString("name")));
        claims.Add(new Claim(ClaimTypes.Email,
            jsonDoc.RootElement.GetString("emailAddress")));
        return claims;
    }

    /// <summary>
    /// Query external authentication provider to get an access token
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    protected virtual async Task<(string code, string state)> GetAccessToken(string code)
    {
        string state = Context.Request.Query["state"];

        // Send the request to the authentication service
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        HttpResponseMessage response = await httpClient
            .PostAsJsonAsync(Options.ExchangeUrl, new
            {
                code,
                redirect_uri = Options.RedirectRoot + Options.RedirectPath,
                client_id = Options.ClientId,
                client_secret = Options.ClientSecret,
                state,
                grant_type = "authorization_code"
            });

        string jsonData = await response.Content.ReadAsStringAsync();
        JsonDocument jsonDoc = JsonDocument.Parse(jsonData);
        string error = jsonDoc.RootElement.GetString("error");
        if (error != null)
        {
            ErrorMessage = "Access Token Error";
            _logger.LogError(ErrorMessage);
            _logger.LogError(jsonData);
        }

        string token = jsonDoc.RootElement.GetString("access_token");
        string jsonState = jsonDoc.RootElement.GetString("state") ?? state;
        return error == null ? (token, state) : (null, null);
    }


    protected virtual Task<string> GetAuthenticationCode()
    {
        return Task.FromResult(Context.Request.Query["code"].ToString());
    }

    private Task<string> GetAuthenticationUrl(AuthenticationProperties properties)
    {
        Dictionary<string, string> qs = new Dictionary<string, string>
        {
            { "client_id", Options.ClientId },
            { "redirect_uri", Options.RedirectRoot + Options.RedirectPath },
            { "scope", Options.Scope },
            { "response_type", "code" },
            { "state", PropertiesFormatter.Protect(properties) }
        };

        return Task.FromResult(Options.AuthenticationUrl + QueryString.Create(qs));
    }
}