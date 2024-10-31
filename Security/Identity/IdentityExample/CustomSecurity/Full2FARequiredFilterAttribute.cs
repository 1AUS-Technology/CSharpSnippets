using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityExample.CustomSecurity;

public class Full2FARequiredFilterAttribute : Attribute, IAsyncPageFilter, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        IActionResult? result = await ApplyPolicy(context.HttpContext);
        if (result != null)
        {
            context.Result = result;
        }
        else
        {
            await next.Invoke();
        }
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        IActionResult? result = await ApplyPolicy(context.HttpContext);
        if (result != null)
        {
            context.Result = result;
        }
        else
        {
            await next.Invoke();
        }
    }

    private async Task<IActionResult?> ApplyPolicy(HttpContext context)
    {
        IAuthorizationService? authorizationService = context.RequestServices.GetService<IAuthorizationService>();
        if (!(await authorizationService.AuthorizeAsync(context.User, "Full2FARequired")).Succeeded)
        {
            return new RedirectToPageResult("/Full2FARequired",
                new { returnUrl = Path(context) });
        }

        return null;
    }

    private string Path(HttpContext context)
    {
        return $"{context.Request.Path}{context.Request.QueryString}";
    }
}