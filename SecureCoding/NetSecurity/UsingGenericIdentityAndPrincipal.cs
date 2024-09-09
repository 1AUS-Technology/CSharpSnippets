using System.Security.Principal;

namespace SecureCoding.NetSecurity;

public class UsingGenericIdentityAndPrincipal
{
    public static void Run()
    {
        GenericIdentity identity = new GenericIdentity("cuti", "secexp");
        string[] myRoles = ["ceo", "admin"];

        GenericPrincipal principal = new GenericPrincipal(identity, myRoles);

        //Attach the generic principal to the current thread

        Thread.CurrentPrincipal = principal;

        String name = principal.Identity.Name;
        bool auth = principal.Identity.IsAuthenticated;
        bool isInRole = principal.IsInRole("Admin");

        Console.WriteLine("The name is: {0}", name);
        Console.WriteLine("The isAuthenticated is: {0}", auth);
        Console.WriteLine("Is this a Admin? {0}", isInRole);
    }
}