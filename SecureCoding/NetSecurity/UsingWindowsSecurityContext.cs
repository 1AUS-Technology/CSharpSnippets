using System.Security.Principal;

namespace SecureCoding.NetSecurity;

public class UsingWindowsSecurityContext
{
    public static void Run()
    {
        // Set the security context to use windows accounts
        AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

        // Get the principal associated with the current Windows user
        WindowsPrincipal? currentUserPrincipal = Thread.CurrentPrincipal as WindowsPrincipal;

        if (currentUserPrincipal != null)
        {
            Console.WriteLine("user is " + currentUserPrincipal.Identity.Name);
        }
        else
        {
            Console.WriteLine("Unknown user");
        }

        // or can get windows identity directly

        WindowsIdentity identity = WindowsIdentity.GetCurrent(TokenAccessLevels.AllAccess);
        WindowsPrincipal principalFromIdentity = new WindowsPrincipal(identity);



    }
}