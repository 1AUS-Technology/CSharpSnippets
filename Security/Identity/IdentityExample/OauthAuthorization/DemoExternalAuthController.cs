using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.OauthAuthorization;

internal class UserRecord
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Code { get; set; }
    public string Token { get; set; }
}

public class ExternalAuthInfo
{
    public string client_id { get; set; }
    public string client_secret { get; set; }
    public string redirect_uri { get; set; }
    public string scope { get; set; }
    public string state { get; set; }
    public string response_type { get; set; }
    public string grant_type { get; set; }
    public string code { get; set; }
}

public class DemoExternalAuthController : Controller
{
    private static readonly string expectedID = "MyClientID";
    private static readonly string expectedSecret = "MyClientSecret";

    private static readonly List<UserRecord?> users = new()
    {
        new UserRecord
        {
            Id = "1", Name = "Alice", EmailAddress = "alice@example.com",
            Password = "myexternalpassword"
        },
        new UserRecord
        {
            Id = "2", Name = "Dora", EmailAddress = "dora@example.com",
            Password = "myexternalpassword"
        }
    };

    private readonly Dictionary<string, string> data
        = new()
        {
            { "token1", "This is Alice's external data" },
            { "token2", "This is Dora's external data" }
        };

    public IActionResult Authenticate([FromQuery] ExternalAuthInfo info)
    {
        return expectedID == info.client_id
            ? View((info, string.Empty))
            : View((info, "Unknown Client"));
    }

    [HttpPost]
    public IActionResult Authenticate(ExternalAuthInfo info, string email,
        string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email and password required");
        }
        else
        {
            UserRecord? user = users.FirstOrDefault(u => u.EmailAddress.Equals(email) && u.Password.Equals(password));
            if (user != null)
            {
                return Redirect(info.redirect_uri
                                + $"?code={user.Code}&scope={info.scope}"
                                + $"&state={info.state}");
            }

            ModelState.AddModelError("", "Email or password incorrect");
        }

        return View((info, ""));
    }


    [HttpPost]
    public IActionResult Exchange([FromBody] ExternalAuthInfo info)
    {
        UserRecord user = users.FirstOrDefault(user => user.Code.Equals(info.code));
        if (user == null || info.client_id != expectedID
                         || info.client_secret != expectedSecret)
        {
            return Json(new { error = "unauthorized_client" });
        }

        return Json(new
        {
            access_token = user.Token,
            expires_in = 3600,
            scope = "openid+email+profile",
            token_type = "Bearer",
            info.state
        });
    }

    [HttpGet]
    public IActionResult Data([FromHeader] string authorization)
    {
        string token = authorization?[7..];
        UserRecord user = users.FirstOrDefault(user => user.Token.Equals(token));
        if (user != null)
        {
            return Json(new { user.Id, user.EmailAddress, user.Name });
        }

        return Json(new { error = "invalid_token" });
    }

    [HttpGet]
    public IActionResult GetData([FromHeader] string authorization)
    {
        if (!string.IsNullOrEmpty(authorization))
        {
            string token = authorization?[7..];
            if (!string.IsNullOrEmpty(token) && data.ContainsKey(token))
            {
                return Json(new { data = data[token] });
            }
        }

        return NotFound();
    }
}