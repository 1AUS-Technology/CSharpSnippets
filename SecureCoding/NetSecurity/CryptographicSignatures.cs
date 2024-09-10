using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SecureCoding.NetSecurity;

public class CryptographicSignatures
{
    public void Run()
    {
        string input = "Hello cuti";
        byte[] signedHash = GenerateSignedHash(input);

        VerifySignature(signedHash, input);
    }

    private void VerifySignature(byte[] signedHash, string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] data = Encoding.ASCII.GetBytes(input);
        byte[] hash = sha256.ComputeHash(data);

        using (RSA rsa = RSA.Create())
        {
            // TODO: import the public key

            RSAPKCS1SignatureDeformatter signatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);

            if (signatureDeformatter.VerifySignature(hash, signedHash))
            {
                Console.WriteLine("The signature is valid.");
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }
        }

    }

    private byte[] GenerateSignedHash(string input)
    {
        var hasher = SHA256.Create();
        byte[] data = Encoding.ASCII.GetBytes(input );
        byte[] hash = hasher.ComputeHash(data);


        // Generate Signature

        byte[] signedHash;

        using (RSA rsa = RSA.Create())
        {
            RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
            rsaFormatter.SetHashAlgorithm(nameof(SHA256));

            signedHash = rsaFormatter.CreateSignature(hash);
        }

        return signedHash;
    }
}