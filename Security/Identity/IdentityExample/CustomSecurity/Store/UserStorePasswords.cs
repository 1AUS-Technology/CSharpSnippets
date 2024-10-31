using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserPasswordStore<AppUser>
{
    public Task<string> GetPasswordHashAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(AppUser user, CancellationToken token)
    {
        return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
    }

    public Task SetPasswordHashAsync(AppUser user, string passwordHash,
        CancellationToken token)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }
}