using System.Security.Cryptography;

namespace SecureCoding.NetSecurity;

public class StoreAsymmetricKeys
{
    public static void Run()
    {
        string containerName = "testkeycontainer";
        GenerateAndSaveKeyToContainer(containerName);

        // Retrieve the key from the container.
        GetKeyFromContainer(containerName);

        DeleteKeyFromContainer(containerName);
    }

    private static void GetKeyFromContainer(string containerName)
    {
        var parameters = new CspParameters()
        {
            KeyContainerName = containerName
        };

        using var rsa = new RSACryptoServiceProvider(parameters);
        Console.WriteLine($"Key retrieved from {rsa.CspKeyContainerInfo.KeyContainerName}");

    }

    private static void GenerateAndSaveKeyToContainer(string containerName)
    {
        var parameters = new CspParameters()
        {
            KeyContainerName = containerName
        };

        using var rsa = new RSACryptoServiceProvider(parameters);

        // Display the key information to the console.
        Console.WriteLine($"Key added to container: \n  {rsa.ToXmlString(true)}");
    }

    private static void DeleteKeyFromContainer(string containerName)
    {
        // Create the CspParameters object and set the key container
        // name used to store the RSA key pair.
        var parameters = new CspParameters
        {
            KeyContainerName = containerName
        };

        // Create a new instance of RSACryptoServiceProvider that accesses
        // the key container.
        using var rsa = new RSACryptoServiceProvider(parameters);
        // Delete the key entry in the container.
        rsa.PersistKeyInCsp = false;

        // Call Clear to release resources and delete the key from the container.
        rsa.Clear();

        Console.WriteLine("Key deleted.");
    }
}