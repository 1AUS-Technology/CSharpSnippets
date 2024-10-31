using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserAuthenticatorKeyStore<AppUser>
{
    public Task SetAuthenticatorKeyAsync(AppUser user, string key, CancellationToken cancellationToken)
    {
        user.AuthenticatorKey = key;
        return Task.CompletedTask;

    }

    public Task<string?> GetAuthenticatorKeyAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.AuthenticatorKey);
    }
}