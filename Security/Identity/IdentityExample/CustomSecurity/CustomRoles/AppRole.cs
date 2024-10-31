using System.Security.Claims;

namespace IdentityExample.CustomSecurity.CustomRoles;

public class AppRole
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public IEnumerable<Claim?> Claims { get; set; }
}