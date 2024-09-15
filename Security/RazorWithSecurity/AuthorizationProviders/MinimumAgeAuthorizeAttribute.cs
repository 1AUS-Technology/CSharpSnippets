using Microsoft.AspNetCore.Authorization;

namespace RazorWithSecurity.AuthorizationProviders;

public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute
{
    const string POLICY_PREFIX = "MinimumAge";

    public MinimumAgeAuthorizeAttribute(int age) => Age = age;

    // Get or set the Age property by manipulating the underlying Policy property
    public int Age
    {
        get
        {
            // get the value after MinimumAge
            var ageString = Policy.Substring(POLICY_PREFIX.Length);
            if (int.TryParse(ageString, out var age))
            {
                return age;
            }

            return default(int);
        }
        set { Policy = $"{POLICY_PREFIX}{value.ToString()}"; }
    }
}