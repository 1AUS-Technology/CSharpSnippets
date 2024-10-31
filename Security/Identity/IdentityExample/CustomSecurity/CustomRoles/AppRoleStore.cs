using System.Collections.Concurrent;
using System.Security.Claims;
using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.CustomRoles;

public partial class AppRoleStore : IRoleStore<AppRole>
{
    private readonly ConcurrentDictionary<string, AppRole> roles = new();

    public AppRoleStore(ILookupNormalizer normalizer)
    {
        Normalizer = normalizer;
        SeedStore();
    }

    public ILookupNormalizer Normalizer { get; set; }

    private IdentityResult Error => IdentityResult.Failed(new IdentityError
    {
        Code = "StorageFailure",
        Description = "Role Store Error"
    });

    public Task<IdentityResult> CreateAsync(AppRole role,
        CancellationToken token)
    {
        if (!roles.ContainsKey(role.Id) && roles.TryAdd(role.Id, role))
        {
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(Error);
    }

    public Task<IdentityResult> DeleteAsync(AppRole role,
        CancellationToken token)
    {
        if (roles.ContainsKey(role.Id) && roles.TryRemove(role.Id, out role))
        {
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(Error);
    }

    public Task<string> GetRoleIdAsync(AppRole role, CancellationToken token)
    {
        return Task.FromResult(role.Id);
    }

    public Task<string> GetRoleNameAsync(AppRole role, CancellationToken token)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(AppRole role, string roleName,
        CancellationToken token)
    {
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(AppRole role,
        CancellationToken token)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(AppRole role, string normalizedName,
        CancellationToken token)
    {
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }


    public Task<IdentityResult> UpdateAsync(AppRole role,
        CancellationToken token)
    {
        if (roles.ContainsKey(role.Id))
        {
            roles[role.Id].UpdateFrom(role);
            return Task.FromResult(IdentityResult.Success);
        }

        return Task.FromResult(Error);
    }

    public void Dispose()
    {
        // do nothing
    }

    private void SeedStore()
    {
        var roleData = new List<string>
        {
            "Administrator", "User", "Sales", "Support"
        };
        var claims = new Dictionary<string, IEnumerable<Claim>>
        {
            {
                "Administrator", new[]
                {
                    new Claim("AccessUserData", "true"),
                    new Claim(ClaimTypes.Role, "Support")
                }
            },
            { "Support", new[] { new Claim(ClaimTypes.Role, "User") } }
        };
        int idCounter = 0;
        foreach (string roleName in roleData)
        {
            AppRole role = new AppRole
            {
                Id = (++idCounter).ToString(),
                Name = roleName,
                NormalizedName = Normalizer.NormalizeName(roleName)
            };
            if (claims.ContainsKey(roleName))
            {
                role.Claims = claims[roleName].ToList();
            }

            roles.TryAdd(role.Id, role);
        }
    }
}

public partial class AppRoleStore : IQueryableRoleStore<AppRole>
{
    public Task<AppRole> FindByIdAsync(string id, CancellationToken token)
    {
        return Task.FromResult(roles.ContainsKey(id) ? roles[id].Clone() : null);
    }

    public Task<AppRole> FindByNameAsync(string name, CancellationToken token)
    {
        return Task.FromResult(roles.Values.FirstOrDefault(r => r.NormalizedName ==
                                                                name)?.Clone());
    }

    public IQueryable<AppRole> Roles => roles.Values.Select(role => role.Clone()).AsQueryable();
}