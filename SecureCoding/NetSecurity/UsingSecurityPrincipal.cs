using System.Security.Principal;

namespace SecureCoding.NetSecurity;

public class UsingSecurityPrincipal
{
    public static void Run()
    {
        //DemandAdminPermission();

        ShowRolesOfPrincipal();
    }

    private static void ShowRolesOfPrincipal()
    {
        AppDomain myDomain = Thread.GetDomain();
        // Set to use windows principal
        myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

        WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

        Console.WriteLine("Checking using WindowsBuiltInRole enum");
        Console.WriteLine($"{myPrincipal.Identity.Name} belongs to");

        var builtInRoles = Enum.GetValues<WindowsBuiltInRole>();

        foreach (var builtInRole in builtInRoles)
        {
            try
            {
                Console.WriteLine($"{builtInRole}? {myPrincipal.IsInRole(builtInRole)}");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{builtInRole}: Cannot check this role");
            }
        }

        // Get the role using the WindowsBuiltInRole enumeration value.
        Console.WriteLine("{0}? {1}.", WindowsBuiltInRole.Administrator, myPrincipal.IsInRole(WindowsBuiltInRole.Administrator));

        Console.WriteLine("Checking using group names as strings");
        // Check administrators
        Console.WriteLine($"Administrators? {myPrincipal.IsInRole("BUILTIN\\Administrators")}");
        //Check users
        Console.WriteLine($"Users? {myPrincipal.IsInRole("BUILTIN\\Users")}");

        
        Console.WriteLine("Checking using SecurityIdentifier");
        //Get the role well-known SID type
        SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

        Console.WriteLine($"WellKnownSidType BuiltinAdministratorsSid {sid.Value}? {myPrincipal.IsInRole(sid)}");
    }

    private static void DemandAdminPermission()
    {
        //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        //PrincipalPermission principalPermission = new PrincipalPermission(null, "Administrators");
        //principalPerm.Demand();
        //Console.WriteLine("Demand succeeded.");
    }
}