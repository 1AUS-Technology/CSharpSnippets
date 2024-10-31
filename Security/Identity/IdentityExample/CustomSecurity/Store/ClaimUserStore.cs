using System.Security.Claims;
using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserClaimStore<AppUser>
{
    public Task AddClaimsAsync(AppUser user, IEnumerable<Claim> claims,
        CancellationToken token)
    {
        foreach (Claim claim in claims)
        {
            user.Claims.Add(claim);
        }

        return Task.CompletedTask;
    }

    public Task<IList<Claim>> GetClaimsAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.Claims);
    }

    public Task RemoveClaimsAsync(AppUser user, IEnumerable<Claim> claims, CancellationToken token)
    {
        foreach (Claim c in user.Claims.Intersect(claims).ToList())
        {
            user.Claims.Remove(c);
        }

        return Task.CompletedTask;
    }

    public async Task ReplaceClaimAsync(AppUser user, Claim oldclaim,
        Claim newClaim, CancellationToken token)
    {
        await RemoveClaimsAsync(user, new[] { oldclaim }, token);

        user.Claims.Add(newClaim);
    }

    public Task<IList<AppUser>> GetUsersForClaimAsync(Claim claim,
        CancellationToken token)
    {
        return Task.FromResult(Users.Where(u => u.Claims.Any(c => Equals(c, claim))).ToList() as IList<AppUser>);
    }

    public bool Equals(Claim first, Claim second)
    {
        return first.Type == second.Type && string.Equals(first.Value, second.Value, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(Claim claim)
    {
        return claim.Type.GetHashCode() + claim.Value.GetHashCode();
    }
}