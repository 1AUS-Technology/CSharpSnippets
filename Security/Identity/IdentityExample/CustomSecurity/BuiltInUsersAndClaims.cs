using System.Security.Claims;

namespace IdentityExample.CustomSecurity;

public static class BuiltInUsersAndClaims
{
    public static readonly Dictionary<string, IEnumerable<string>> UserAndRoles
        = new()
        {
            { "Alice", new[] { "User", "Administrator" } },
            { "Bob", new[] { "User" } },
            { "Charlie", new[] { "User" } }
        };

    public static string[] Users => UserAndRoles.Keys.ToArray();

    public static Dictionary<string, IEnumerable<Claim>> Claims => UserAndRoles.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(role => new Claim(ClaimTypes.Role, role)), StringComparer.InvariantCultureIgnoreCase);
}