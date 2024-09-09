using System.Security.Principal;

namespace SecureCoding.NetSecurity;

public class ImpersonatingAndReverting
{
    public static void Run()
    {
        //      Sometimes you might need to obtain a Windows account token to impersonate a Windows account.For example, your ASP.NET - based application might have to act on behalf of several users at different times.Your application might accept a token that represents an administrator from Internet Information Services(IIS), impersonate that user, perform an operation, and revert to the previous identity.
        //      Next, it might accept a token from IIS that represents a user with fewer rights, perform some operation, and revert again.
        //      In situations where your application must impersonate a Windows account that has not been attached to the current thread by IIS, you must retrieve that account's token and use it to activate the account. You can do this by performing the following tasks:

        //1 . Retrieve an account token for a particular user by making a call to the unmanaged LogonUser method. This method is not in the .NET base class library, but is located in the unmanaged advapi32.dll. 

        //string logonToken = "";
        //WindowsIdentity impersonateIdentity = new WindowsIdentity(logonToken);

        //WindowsImpersonationContext impersonation = impersonateIdentity.Impersonate();
        //impersonation.Undo();
    }
}