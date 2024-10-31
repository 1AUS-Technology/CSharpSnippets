using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserPhoneNumberStore<AppUser>
{
    public Task<string> GetPhoneNumberAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.PhoneNumber);
    }

    public Task SetPhoneNumberAsync(AppUser user, string phoneNumber,
        CancellationToken token)
    {
        user.PhoneNumber = phoneNumber;
        return Task.CompletedTask;
    }

    public Task<bool> GetPhoneNumberConfirmedAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.PhoneNumberConfirmed);
    }

    public Task SetPhoneNumberConfirmedAsync(AppUser user, bool confirmed,
        CancellationToken token)
    {
        user.PhoneNumberConfirmed = confirmed;
        return Task.CompletedTask;
    }
}