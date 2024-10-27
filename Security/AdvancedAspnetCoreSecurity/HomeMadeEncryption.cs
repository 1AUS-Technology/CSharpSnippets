using System.Security.Cryptography;

namespace AdvancedAspnetCoreSecurity;

public class HomeMadeEncryption
{
    private const string InitialisationVector = "zP6xLOlBdTThKmXX";

    /// <summary>
    /// The key to use when encrypting and decrypting strings. If this value ever changes then any strings
    /// that have been previously encrypted will not be able to be decrypted
    /// </summary>
    private static readonly byte[] EncryptionKey = [40, 137, 218, 23, 105, 231, 179, 42, 89, 102, 126, 154, 218, 32, 126, 240, 45, 247, 110, 141, 198, 0, 226, 57];

    public static void Run()
    {
        using (RijndaelManaged myRijndael = new RijndaelManaged())
        {
            myRijndael.BlockSize = 128;
            myRijndael.KeySize = 192;

            myRijndael.GenerateKey();
            myRijndael.GenerateIV();

            byte[] iv = myRijndael.IV;

            var hex = myRijndael.IV.BytesToHex();

            Console.WriteLine("IV Generated: " + hex);
            Console.WriteLine($"IV QR: {InitialisationVector}");

            Console.WriteLine("IV Length");

            Console.WriteLine("IV Generated : " + hex.Length);

            Console.WriteLine("IV QR Length: " + InitialisationVector.Length);

            var key = myRijndael.Key;

            Console.WriteLine("Key Length: " + key.Length);
            Console.WriteLine("QR Key Length: " + EncryptionKey.Length);

            Console.WriteLine("Key:");

            foreach (byte b in key)
            {
                Console.Write(b + ",");
            }

        }


    }
}