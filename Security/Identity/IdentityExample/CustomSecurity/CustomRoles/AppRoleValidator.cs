using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.CustomRoles;

public class AppRoleValidator : IRoleValidator<AppRole>
{
    public async Task<IdentityResult> ValidateAsync(RoleManager<AppRole> manager, AppRole role)
    {
        // The validator prevents role names that simply add or omit the letter s from the name of an existing role
        // Admin and Admins are the same role
        if (await manager.FindByNameAsync(role.Name.EndsWith("s") ? role.Name[..^1] : role.Name + "s") == null)
        {
            return IdentityResult.Success;
        }
        return IdentityResult.Failed(error);
    }

    private static IdentityError error = new IdentityError
    {
        Description = "Names cannot be plural/singular of existing roles"
    };
}