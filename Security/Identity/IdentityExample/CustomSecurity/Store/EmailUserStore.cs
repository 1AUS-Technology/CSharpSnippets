using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserEmailStore<AppUser>
{
    public Task<AppUser> FindByEmailAsync(string normalizedEmail,
        CancellationToken token)
    {
        return Task.FromResult(Users.FirstOrDefault(user => user.NormalizedEmailAddress == normalizedEmail));
    }

    public Task<string> GetEmailAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.EmailAddress);
    }

    public Task SetEmailAsync(AppUser user, string email,
        CancellationToken token)
    {
        user.EmailAddress = email;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedEmailAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.NormalizedEmailAddress);
    }

    public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail,
        CancellationToken token)
    {
        user.NormalizedEmailAddress = normalizedEmail;
        return Task.CompletedTask;
    }

    public Task<bool> GetEmailConfirmedAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.EmailAddressConfirmed);
    }

    public Task SetEmailConfirmedAsync(AppUser user, bool confirmed,
        CancellationToken token)
    {
        user.EmailAddressConfirmed = confirmed;
        return Task.CompletedTask;
    }
}