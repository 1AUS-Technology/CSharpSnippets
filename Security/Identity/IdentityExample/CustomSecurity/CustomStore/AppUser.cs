using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.CustomStore;

public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }

    public string EmailAddress { get; set; }
    public string NormalizedEmailAddress { get; set; }
    public bool EmailAddressConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string FavoriteFood { get; set; }
    public string Hobby { get; set; }
    public IList<Claim> Claims { get; set; }
    public string SecurityStamp { get; set; }
    public string PasswordHash { get; set; }
    public bool TwoFactorEnabled { get; set; }

    public bool CanUserBeLockedout { get; set; } = true;
    public int FailedSignInCount { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }

    public bool AuthenticatorEnabled { get; set; }
    public string AuthenticatorKey { get; set; }
    public IList<UserLoginInfo> UserLogins { get; set; }

    // Identity provides the AuthenticationToken class, which defines Name and Value properties.
    // To store tokens, I need to be able to keep track of the source of each token, 
    public IList<(string provider, AuthenticationToken token)> AuthTokens { get; set; } = new List<(string provider, AuthenticationToken token)>();
}