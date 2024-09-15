using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace RazorWithSecurity.AuthorizationProviders;

public class MinimumAgePolicyProvider : IAuthorizationPolicyProvider
{
    const string POLICY_PREFIX = "MinimumAge";

    // Policies are looked up by string name, so expect 'parameters' (like age)
    // to be embedded in the policy names. This is abstracted away from developers
    // by the more strongly-typed attributes derived from AuthorizeAttribute
    // (like [MinimumAgeAuthorize()] in this sample)
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) && int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var age))
        {
            var policyBuilder = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);
            policyBuilder.AddRequirements(new MinimumAgeRequirement(age));

            return Task.FromResult(policyBuilder.Build());
        }
        return Task.FromResult<AuthorizationPolicy>(null);
    }

    //  provide an authorization policy for [Authorize] attributes without a policy name specified
    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return Task.FromResult(new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

    }

    // to provide a policy that's used when combining policies and when no policies are specified.
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
       return  Task.FromResult<AuthorizationPolicy>(null);
    }
}

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public MinimumAgeRequirement(int age)
    {
        
    }
}