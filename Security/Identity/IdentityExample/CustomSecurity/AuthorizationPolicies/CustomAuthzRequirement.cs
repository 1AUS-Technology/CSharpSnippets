using Microsoft.AspNetCore.Authorization;

namespace IdentityExample.CustomSecurity.AuthorizationPolicies;

public class CustomAuthzRequirement: IAuthorizationRequirement
{
    public string Name { get; set; }
}