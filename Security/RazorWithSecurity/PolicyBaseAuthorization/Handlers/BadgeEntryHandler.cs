using Microsoft.AspNetCore.Authorization;

namespace RazorWithSecurity.PolicyBaseAuthorization.Handlers;

public class BadgeEntryHandler : AuthorizationHandler<BuildingEntryRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BuildingEntryRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "BadgeId" && c.Issuer == "https://vtsecurity.com"))
        {
            context.Succeed(requirement);
        }
        
        // Has not failed here because other handler can check.
        // In a case &&, then this handler must fail
        return Task.CompletedTask;
    }
}