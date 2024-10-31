using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserAuthenticationTokenStore<AppUser>
{
    public Task SetTokenAsync(AppUser user, string loginProvider, string name, string? value, CancellationToken cancellationToken)
    {
        user.AuthTokens.Add((loginProvider, new AuthenticationToken
        {
            Name = name,
            Value = value
        }));

        return Task.CompletedTask;
    }

    public Task RemoveTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        if (user.AuthTokens != null)
        {
            user.AuthTokens = user.AuthTokens.Where(t => t.provider != loginProvider && t.token.Name != name).ToList();
        }

        return Task.CompletedTask;
    }

    public Task<string?> GetTokenAsync(AppUser user, string loginProvider, string name, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.AuthTokens?.FirstOrDefault(t => t.provider == loginProvider && t.token.Name == name).token.Value);
    }
}