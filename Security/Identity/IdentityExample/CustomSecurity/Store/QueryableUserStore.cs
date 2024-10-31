using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;
namespace IdentityExample.CustomSecurity.Store;

public partial class AppUserStore : IQueryableUserStore<AppUser>
{
    public IQueryable<AppUser> Users => users.Values.Select(user => user.Clone()).AsQueryable();
}