using SecureCoding.NetSecurity;

namespace SecureCoding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //UsingSecurityPrincipal.Run();

            //UsingWindowsSecurityContext.Run();
            UsingGenericIdentityAndPrincipal.Run();
            Console.ReadLine();
        }
    }
}
