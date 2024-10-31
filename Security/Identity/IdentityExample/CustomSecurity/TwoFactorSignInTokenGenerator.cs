using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity;

public class TwoFactorSignInTokenGenerator : SimpleTokenGenerator
{
    protected override int CodeLength => 3;

    public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser> manager, AppUser user)
    {
        return Task.FromResult(user.TwoFactorEnabled);
    }
}