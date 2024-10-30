using Microsoft.AspNetCore.Authorization;

namespace IdentityExample.CustomSecurity.AuthorizationPolicies;

public class CustomRequirementHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        foreach (var customRequirement in context.PendingRequirements.OfType<CustomAuthzRequirement>())
        {
            if (context.User.Identities.Any(it => it.Name == customRequirement.Name))
            {
                context.Succeed(customRequirement);
            }
        }

        return Task.CompletedTask;
    }
}