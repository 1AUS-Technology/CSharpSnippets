using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserLoginStore<AppUser>
{
    public Task AddLoginAsync(AppUser user, UserLoginInfo login, CancellationToken cancellationToken)
    {
        if (user.UserLogins == null)
        {
            user.UserLogins = new List<UserLoginInfo>();
        }

        user.UserLogins.Add(login);
        return Task.CompletedTask;
    }

    public async Task RemoveLoginAsync(AppUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        user.UserLogins = (await GetLoginsAsync(user, cancellationToken)).Where(login
            => !login.LoginProvider.Equals(loginProvider)
               && !login.ProviderKey.Equals(providerKey)).ToList();
    }

    public Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserLogins ?? new List<UserLoginInfo>());
    }

    public Task<AppUser?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        return Task.FromResult(Users.FirstOrDefault(u => u.UserLogins != null &&
                                                         u.UserLogins.Any(login => login.LoginProvider.Equals(loginProvider)
                                                                                   && login.ProviderKey.Equals(providerKey))));
    }
}