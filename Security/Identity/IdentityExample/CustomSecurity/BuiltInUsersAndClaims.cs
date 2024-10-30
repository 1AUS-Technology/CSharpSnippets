using System.Security.Claims;
using IdentityExample.CustomSecurity.AuthorizationHandlers;

namespace IdentityExample.CustomSecurity;

public static class BuiltInUsersAndClaims
{
    public static string[] Schemes = new string[] {AuthenticationSchemes.TestScheme };
    public static readonly Dictionary<string, IEnumerable<string>> UserAndRoles
        = new()
        {
            { "Alice", new[] { "User", "Administrator" } },
            { "Bob", new[] { "User" } },
            { "Charlie", new[] { "User" } }
        };

    public static string[] Users => UserAndRoles.Keys.ToArray();

    public static Dictionary<string, IEnumerable<Claim>> Claims => UserAndRoles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(role => new Claim(ClaimTypes.Role, role)), StringComparer.InvariantCultureIgnoreCase);

    public static IEnumerable<ClaimsPrincipal> GetUsers()
    {
        foreach (var scheme in Schemes)
        {
            foreach (var kvp in Claims)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(scheme);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, kvp.Key));
                claimsIdentity.AddClaims(kvp.Value);
                yield return new ClaimsPrincipal(claimsIdentity);
            }
        }
    }
}