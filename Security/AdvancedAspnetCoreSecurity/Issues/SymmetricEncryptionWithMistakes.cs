using System.Security.Cryptography;
using System.Text;

namespace AdvancedAspnetCoreSecurity.Issues;

public class SymmetricEncryptionWithMistakes
{
    public static byte[] Encrypt(string plainText, byte[] encryptionKey, byte[] initialVector)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);

        AesManaged aes = new AesManaged();
        
        ICryptoTransform encryptor = aes.CreateEncryptor(encryptionKey, initialVector);

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(inputBytes, 0, inputBytes.Length);
        cryptoStream.FlushFinalBlock();
        return memoryStream.ToArray();
    }

    public static string Decrypt(byte[] encryptedData, byte[] encryptionKey, byte[] initialVector)
    {
        AesManaged aes = new AesManaged();

        ICryptoTransform encryptor = aes.CreateDecryptor(encryptionKey, initialVector);

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(encryptedData, 0, encryptedData.Length);
        cryptoStream.FlushFinalBlock();

        byte[] decryptedBytes = memoryStream.ToArray();

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}