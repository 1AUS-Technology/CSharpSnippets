using System.Net.Http.Headers;
using System.Text.Json;
using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityExample.Models;

public class UseStoredAccessTokenModel : PageModel
{
    public UseStoredAccessTokenModel(UserManager<AppUser> userManager)
    {
        UserManager = userManager;
    }

    public UserManager<AppUser> UserManager { get; set; }
    public string? Data { get; set; } = "No Data";

    public async Task OnGetAsync()
    {
        AppUser user = await UserManager.GetUserAsync(HttpContext.User);
        
        if (user != null)
        {
            string storedToken = await UserManager.GetAuthenticationTokenAsync(user, "demoAuth", "access_token");

            if (!string.IsNullOrEmpty(storedToken))
            {
                HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/api/DemoExternalApi");

                msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", storedToken);
                HttpResponseMessage response = await new HttpClient().SendAsync(msg);
                JsonDocument doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                Data = doc.RootElement.GetString();
            }
        }
    }
}