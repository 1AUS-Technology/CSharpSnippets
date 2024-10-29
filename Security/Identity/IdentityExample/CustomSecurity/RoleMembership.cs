using System.Security.Claims;
using System.Security.Principal;

namespace IdentityExample.CustomSecurity;

public class RoleMembership(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        IIdentity mainIdentity = context.User.Identity;
        if (mainIdentity.IsAuthenticated && BuiltInUsersAndClaims.Claims.ContainsKey(mainIdentity.Name))
        {
            ClaimsIdentity identity = new ClaimsIdentity("Roles-Authentication-Type");
            identity.AddClaim(new Claim(ClaimTypes.Name, mainIdentity.Name));
            identity.AddClaims(BuiltInUsersAndClaims.Claims[mainIdentity.Name]);

            context.User.AddIdentity(identity);
        }

        await next(context);
    }
}

