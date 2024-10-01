using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CertificateManagements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintStoreNameAndLocations();

            string storeName = "Root";
            PrintStoreCertificates(storeName, StoreLocation.CurrentUser);
        }

        private static void PrintStoreCertificates(string storeName, StoreLocation location)
        {
            var store = new X509Store(storeName, location);
            store.Open(OpenFlags.ReadOnly);

            foreach (var storeCertificate in store.Certificates)
            {
                Console.WriteLine("Certificate Name: {0}", storeCertificate.IssuerName.Name);
            }

        }

        private static void PrintStoreNameAndLocations()
        {
            Console.WriteLine("\r\nExists Certs Name and Location");
            Console.WriteLine("------ ----- -------------------------");

            foreach (StoreLocation storeLocation in (StoreLocation[])
                     Enum.GetValues(typeof(StoreLocation)))
            {
                foreach (StoreName storeName in (StoreName[])
                         Enum.GetValues(typeof(StoreName)))
                {
                    X509Store store = new X509Store(storeName, storeLocation);

                    try
                    {
                        store.Open(OpenFlags.OpenExistingOnly);

                        Console.WriteLine("Yes    {0,4}  {1}, {2}",
                            store.Certificates.Count, store.Name, store.Location);
                    }
                    catch (CryptographicException)
                    {
                        Console.WriteLine("No           {0}, {1}",
                            store.Name, store.Location);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
