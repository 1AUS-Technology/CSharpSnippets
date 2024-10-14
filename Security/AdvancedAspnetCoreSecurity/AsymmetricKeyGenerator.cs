using System.Security.Cryptography;

namespace AdvancedAspnetCoreSecurity;

public static class AsymmetricKeyGenerator
{
    public static AsymmetricKeyPair GenerateKeys()
    {
        using var rsa = new RSACryptoServiceProvider(2048);
        // do not keep the key in the crypto service in Windows
        rsa.PersistKeyInCsp = false;

        var keyPair = new AsymmetricKeyPair
        {
            // Export private key
            PrivateKey = rsa.ToXmlString(true),
            PublicKey = rsa.ToXmlString(false)
        };

        return keyPair;
    }

}

public struct AsymmetricKeyPair
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
}