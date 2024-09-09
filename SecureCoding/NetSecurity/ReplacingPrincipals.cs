using System.Security.Principal;

namespace SecureCoding.NetSecurity;

public class ReplacingPrincipals
{
    public static void Run()
    {
        // applications that require the ability to replace Principal objects must be granted the System.Security.Permissions.SecurityPermission object for principal control.
        // (Note that this permission is not required for performing role-based security checks or for creating Principal objects.) to avoid malicious change principals

        GenericPrincipal genericPrincipal = CreateGenericPrincipalFromWindowsUser();
        // Retrieve the generic identity of the GenericPrincipal object.
        GenericIdentity principalIdentity =
            (GenericIdentity)genericPrincipal.Identity;

        if (principalIdentity.IsAuthenticated)
        {
            Console.WriteLine(principalIdentity.Name);
            Console.WriteLine("Type:" + principalIdentity.AuthenticationType);
        }

        if (genericPrincipal.IsInRole("CanChangePrincipal"))
        {
            Console.WriteLine("Users belong to group that can change principals");
            Thread.CurrentPrincipal = genericPrincipal;
        }

    }

    private static GenericPrincipal CreateGenericPrincipalFromWindowsUser()
    {
        WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
        string[] roles = new string[10];

        if (windowsIdentity.IsAuthenticated)
        {
            roles[0] = "CanChangePrincipal";
        }

        if (windowsIdentity.IsGuest)
        {
            // Add custom GuestUser role.
            roles[1] = "GuestUser";
        }

        if (windowsIdentity.IsSystem)
        {
            // Add custom SystemUser role.
            roles[2] = "SystemUser";
        }

        string authenticationType = windowsIdentity.AuthenticationType!;
        GenericIdentity identity = new GenericIdentity(windowsIdentity.Name, authenticationType);

        return new GenericPrincipal(identity, roles);
    }
}