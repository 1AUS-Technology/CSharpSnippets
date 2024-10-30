using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace IdentityExample.Models;

public class SignOutModel : PageModel
{
    public string Username { get; set; }
    public void OnGet()
    {
        Username = User.Identity.Name ?? "(No Signed In User)";
    }
    public async Task<ActionResult> OnPost()
    {
        await HttpContext.SignOutAsync();
        return RedirectToPage("SignIn");
    }
}