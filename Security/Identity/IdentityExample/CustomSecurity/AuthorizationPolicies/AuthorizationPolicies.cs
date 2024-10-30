using System.Security.Claims;
using IdentityExample.CustomSecurity.AuthorizationHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IdentityExample.CustomSecurity.AuthorizationPolicies;

public class AuthorizationPolicies
{
    public static void AddPolicies(AuthorizationOptions options)
    {
        options.FallbackPolicy = new AuthorizationPolicy(new IAuthorizationRequirement[]
        {
            //new CustomAuthzRequirement
            //{
            //    Name = "Cuti"
            //}
            new RolesAuthorizationRequirement(["UserRole", "AdminRole"]),
            new NameAuthorizationRequirement("Cuti"),
            new AssertionRequirement(context => !string.Equals(context.User.Identity?.Name, "VanBeo", StringComparison.OrdinalIgnoreCase))
        }, [AuthenticationSchemes.TestScheme]);

        options.AddPolicy("UsersExceptBob", new AuthorizationPolicy(
            new IAuthorizationRequirement[] {
                new RolesAuthorizationRequirement(new[] { "User" }),
                new AssertionRequirement(context => !string.Equals(context.User.Identity?.Name, "Bob", StringComparison.OrdinalIgnoreCase))
            }, Enumerable.Empty<string>()));

        options.AddPolicy("AdminOnly", policyConfiguration=> policyConfiguration.RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, "Administrator"));
    }
}