using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityExample.Models;

public class ExternalSignInModel : PageModel
{
    public ExternalSignInModel(SignInManager<AppUser> signInManager)
    {
        SignInManager = signInManager;
    }

    public SignInManager<AppUser> SignInManager { get; set; }

    public IActionResult OnPost(string providerName, string returnUrl = "/")
    {
        string redirectUrl = Url.Page("./ExternalSignIn", "Correlate", new { returnUrl });

        AuthenticationProperties properties = SignInManager.ConfigureExternalAuthenticationProperties(providerName, redirectUrl);
        return new ChallengeResult(providerName, properties);
    }
}