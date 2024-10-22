using System.Security.Cryptography;
using System.Text;

namespace AdvancedAspnetCoreSecurity;

public enum EncryptionAlgorithm
{
    Aes128 = 1
}

public interface ISymmetricEncryptor
{
    string Encrypt(string plainText, string keyName, int keyIndex, EncryptionAlgorithm algorithm);
    string Decrypt(string cipherText, string keyName);
}

public class SymmetricEncryptor(ISecretStore secretStore) : ISymmetricEncryptor
{
    public string Encrypt(string plainText, string keyName, int keyIndex, EncryptionAlgorithm algorithm)
    {
        var keyValue = secretStore.GetKey(keyName, keyIndex);

        return algorithm switch
        {
            EncryptionAlgorithm.Aes128 => EncryptAes(plainText, keyValue, EncryptionAlgorithm.Aes128, keyIndex),
            _ => throw new NotImplementedException(algorithm.ToString())
        };
    }


    public string Decrypt(string cipherText, string keyName)
    {
        var (algorithm, keyIndex, initialVector, tagText, trimmedCipherText) = GetAlgorithm(cipherText);


        return algorithm switch
        {
            EncryptionAlgorithm.Aes128 => DescryptAes(keyName, keyIndex, tagText, trimmedCipherText, initialVector),
            _ => throw new NotImplementedException(algorithm.ToString())
        };
    }

    private string DescryptAes(string keyName, int keyIndex, string tagText, string trimmedCipherText,
        string initialVector)
    {
        var encryptionKey = secretStore.GetKey(keyName, keyIndex);

        var keyBytes = encryptionKey.HexStringToByteArray();
        var tagBytes = tagText.HexStringToByteArray();
        var encryptedBytes = trimmedCipherText.HexStringToByteArray();
        var plainText = new byte[encryptedBytes.Length];

        using (var aes = new AesGcm(keyBytes, AesGcm.TagByteSizes.MaxSize))
        {
            aes.Decrypt(initialVector.HexStringToByteArray(), encryptedBytes, tagBytes, plainText);
        }

        return Encoding.UTF8.GetString(plainText);
    }

    private string EncryptAes(string plainText, string encryptionKey, EncryptionAlgorithm algorithm, int keyIndex)
    {
        var keyBytes = encryptionKey.HexStringToByteArray();
        var ivLength = AesGcm.NonceByteSizes.MaxSize;
        var tagSize = AesGcm.TagByteSizes.MaxSize;

        var initialVector = GenerateRandomBytes(ivLength);
        var tag = new byte[tagSize];

        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = new byte[plainBytes.Length];
        using (var aes = new AesGcm(keyBytes, tagSize))
        {
            aes.Encrypt(initialVector, plainBytes, cipherBytes, tag);
        }

        var encryptedText = cipherBytes.BytesToHex();
        var ivText = initialVector.BytesToHex();
        var tagText = tag.BytesToHex();

        // concatenate all key and IV information for decryption
        return $"[{(int)algorithm}.{keyIndex}][{ivText}.{tagText}]{encryptedText}";
    }

    protected (EncryptionAlgorithm algorithm, int keyIndex, string initialVector, string tagText, string
        trimmedCipherText)
        GetAlgorithm(string cipherText)
    {
        // Check if the cipherText is too short or doesn't start with '['
        if (cipherText.Length <= 5 || cipherText[0] != '[')
        {
            throw new ArgumentException("Invalid encrypted data");
        }

        // Split the cipherText into parts using ']' as delimiter
        var parts = cipherText.Split(']');
        // Ensure we have at least 3 parts: [algorithm.keyIndex][iv.tag]ciphertext
        if (parts.Length < 3)
        {
            throw new ArgumentException("Invalid encrypted data format");
        }

        // Extract and parse algorithm and key index
        var algorithmInfo = parts[0][1..].Split('.'); // Remove leading '[' and split by '.'
        if (algorithmInfo.Length != 2 ||
            !int.TryParse(algorithmInfo[0], out var algorithm) ||
            !int.TryParse(algorithmInfo[1], out var keyIndex))
        {
            throw new ArgumentException("Invalid algorithm or key index");
        }

        // Extract and split IV and tag
        var ivAndTag = parts[1][1..].Split('.'); // Remove leading '[' and split by '.'
        if (ivAndTag.Length != 2)
        {
            throw new ArgumentException("Invalid IV or tag format");
        }

        // Return a tuple with all extracted information
        return (
            (EncryptionAlgorithm)algorithm,
            keyIndex,
            initialVector: ivAndTag[0],
            tagText: ivAndTag[1],
            trimmedCipherText: string.Join(']', parts[2..]) // Rejoin remaining parts as they may contain ']'
        );
    }

    private byte[] GenerateRandomBytes(int blockSize)
    {
        return RandomNumberGenerator.GetBytes(blockSize);
    }
}