using System.Security.Claims;
using IdentityExample.CustomSecurity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;

namespace IdentityExample.Models;

public class SignInModel : PageModel
{
    public SelectList Users => new SelectList(BuiltInUsersAndClaims.Users, User.Identity.Name);
    public string Username { get; set; }
    public int? Code { get; set; }

    public void OnGet(int? code)
    {
        Code = code;
        Username = User.Identity.Name ?? "(No Signed In User)";
    }

    public async Task<ActionResult> OnPost(string userName, [FromQuery]string returnUrl)
    {
        Claim claim = new Claim(ClaimTypes.Name, userName);
        ClaimsIdentity simpleFormIdentity = new ClaimsIdentity("simpleform");
        simpleFormIdentity.AddClaim(claim);

        await HttpContext.SignInAsync(new ClaimsPrincipal(simpleFormIdentity));

        var url = Url.IsLocalUrl(returnUrl) ? returnUrl : "/signin";
        return Redirect(url);
    }
}