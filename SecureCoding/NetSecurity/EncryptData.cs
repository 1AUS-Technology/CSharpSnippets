using System.Security.Cryptography;

namespace SecureCoding.NetSecurity;

public class EncryptData
{
    public static void Run()
    {
        SymmetricEncryptFile();
        AsymmetricEncryption();
    }

    private static void SymmetricEncryptFile()
    {
        var fileStream = File.Open("C://temp//cuti-encrypted.txt", FileMode.CreateNew);
        byte[] key =
        {
            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        };

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            byte[] iv = aes.IV;

            fileStream.Write(iv, 0, iv.Length);
            

            using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (StreamWriter writer = new StreamWriter(cryptoStream))
                {
                    //. is the delimiter for IV
                    writer.Write(".Unencrypted text");
                }
            }
        }

        Console.WriteLine("The file was encrypted.");
    }

    private static void AsymmetricEncryption()
    {
        //Initialize the byte arrays to the public key information.
        byte[] modulus =
        {
            214,46,220,83,160,73,40,39,201,155,19,202,3,11,191,178,56,
            74,90,36,248,103,18,144,170,163,145,87,54,61,34,220,222,
            207,137,149,173,14,92,120,206,222,158,28,40,24,30,16,175,
            108,128,35,230,118,40,121,113,125,216,130,11,24,90,48,194,
            240,105,44,76,34,57,249,228,125,80,38,9,136,29,117,207,139,
            168,181,85,137,126,10,126,242,120,247,121,8,100,12,201,171,
            38,226,193,180,190,117,177,87,143,242,213,11,44,180,113,93,
            106,99,179,68,175,211,164,116,64,148,226,254,172,147
        };

        byte[] exponent = { 1, 0, 1 };

        //Create values to store encrypted symmetric keys.
        byte[] encryptedSymmetricKey;
        byte[] encryptedSymmetricIV;

        //Create a new instance of the RSA class.
        RSA rsa = RSA.Create();

        //Create a new instance of the RSAParameters structure.
        RSAParameters parameters = new RSAParameters();

        parameters.Exponent = exponent;
        parameters.Modulus = modulus;

        // import to Rsa
        rsa.ImportParameters(parameters);

        //Create Aes 
        Aes aes = Aes.Create();
        encryptedSymmetricIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.OaepSHA3_512);
        encryptedSymmetricKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.OaepSHA3_512);
    }
}