using System.Collections.Concurrent;
using System.Security.Claims;
using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IUserStore<AppUser>
{
    private readonly ConcurrentDictionary<string, AppUser> users = new();

    public AppUserStore(ILookupNormalizer normalizer)
    {
        Normalizer = normalizer;
        SeedStore();
    }

    public ILookupNormalizer Normalizer { get; set; }

    private IdentityResult Error => IdentityResult.Failed(new IdentityError
    {
        Code = "StorageFailure",
        Description = "User Store Error"
    });

    public void Dispose()
    {
        users.Clear();
    }

    public Task<string> GetNormalizedUserNameAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task<string> GetUserIdAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string> GetUserNameAsync(AppUser user,
        CancellationToken token)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetNormalizedUserNameAsync(AppUser user,
        string normalizedName, CancellationToken token)
    {
        return Task.FromResult(user.NormalizedUserName = normalizedName);
    }

    public Task SetUserNameAsync(AppUser user, string userName,
        CancellationToken token)
    {
        return Task.FromResult(user.UserName = userName);
    }

    public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        if (users.TryAdd(user.Id, user))
        {
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(IdentityResult.Failed(new IdentityError
        {
            Code = "StorageFailure",
            Description = "Cannot at the user"
        }));
    }

    public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        if (users.AddOrUpdate(user.Id, user, (id, existingUser) => user) == user)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(Error);
    }

    public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        if (users.ContainsKey(user.Id)
            && users.TryRemove(user.Id, out user))
        {
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(Error);
    }

    public Task<AppUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return Task.FromResult(users.GetValueOrDefault(userId)?.Clone());
    }

    public Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return Task.FromResult(users.Values.FirstOrDefault(user => user.NormalizedUserName == normalizedUserName)?.Clone());
    }

    private void SeedStore()
    {
        int idCounter = 0;
        foreach (string name in BuiltInUsersAndClaims.Users)
        {
            AppUser user = new AppUser
            {
                Id = (++idCounter).ToString(),
                UserName = name,
                NormalizedUserName = Normalizer.NormalizeName(name),
                EmailAddress = EmailFromName(name),
                NormalizedEmailAddress =
                    Normalizer.NormalizeEmail(EmailFromName(name)),
                EmailAddressConfirmed = true,
                PhoneNumber = "123-4567",
                PhoneNumberConfirmed = true
            };
            user.Claims = BuiltInUsersAndClaims.UserAndRoles[user.UserName]
                .Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            users.TryAdd(user.Id, user);
        }
    }

    private string EmailFromName(string name)
    {
        return $"{name}@executive.com";
    }
}