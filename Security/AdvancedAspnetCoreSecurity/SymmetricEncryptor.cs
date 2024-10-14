using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AdvancedAspnetCoreSecurity;

public class SymmetricEncryptor
{
    public enum EncryptionAlgorithm
    {
        AES128 = 1
    }

    private readonly EncryptionAlgorithm _defaultAlgorithm = EncryptionAlgorithm.AES128;

    private readonly int _defaultKeyIndex;
    private readonly ISecretStore _secretStore;

    public SymmetricEncryptor(IConfiguration config, ISecretStore secretStore)
    {
        _defaultKeyIndex = config.GetValue<int>("AppSettings:KeyIndex");
        _secretStore = secretStore;
    }

    private int GetBlockSizeInBytes(EncryptionAlgorithm algorithm)
    {
        switch (algorithm)
        {
            case EncryptionAlgorithm.AES128:
                return 16;
            default:
                throw new NotImplementedException($"Cannot find block size for{algorithm.ToString()} algorithm");
        }
    }

    public string Encrypt(string plainText, string keyName, EncryptionAlgorithm algorithm)
    {
        var keyString = _secretStore.GetKey(keyName, _defaultKeyIndex);

        switch (algorithm)
        {
            case EncryptionAlgorithm.AES128:
                return EncryptAES(plainText, keyString, _defaultAlgorithm, _defaultKeyIndex);
            default:
                throw new NotImplementedException(algorithm.ToString());
        }
    }

    private string EncryptAES(string plainText, string encryptionKey, EncryptionAlgorithm algorithm, int keyIndex)
    {
        var keyBytes = encryptionKey.HexStringToByteArray();
        int ivLength = AesGcm.NonceByteSizes.MaxSize;
        int tagSize = AesGcm.TagByteSizes.MaxSize;

        var initialVector = GenerateRandomBytes(ivLength);
        byte[] tag = new byte[tagSize];

        byte[] encryptedBytes = [];
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = new byte[plainBytes.Length];
        using (var aes = new AesGcm(keyBytes, tagSize))
        {
            aes.Encrypt(initialVector, plainBytes, cipherBytes, tag);
        }

        var encryptedText = encryptedBytes.BytesToHex();
        var ivText = initialVector.BytesToHex();
        var tagText = tag.BytesToHex();

        // concatenate all key and IV information for decryption
        return $"[{(int)algorithm}.{keyIndex}][{ivText}.{tagText}]{encryptedText}";
    }


    public string Decrypt(string cipherText, string keyName)
    {
        (int algorithm, int keyIndex, string initialVector, string tagText, string trimmedCipherText) = GetAlgorithm(cipherText);
        var encryptionKey = _secretStore.GetKey(keyName, keyIndex);

        var keyBytes = encryptionKey.HexStringToByteArray();
        var tagBytes = tagText.HexStringToByteArray();

        var encryptedBytes = trimmedCipherText.HexStringToByteArray();
        byte[] plainText = new byte[encryptedBytes.Length];
        using (var aes = new AesGcm(keyBytes, tagBytes.Length))
        {
            aes.Decrypt(initialVector.HexStringToByteArray(), encryptedBytes, tagBytes, plainText);
        }

        return Encoding.UTF8.GetString(plainText);
    }

    protected (int algorithm, int keyIndex, string initialVector, string tagText, string trimmedCipherText) GetAlgorithm(string cipherText)
    {
        if (cipherText.Length <= 5 || cipherText[0] != '[')
        {
            throw new ArgumentException("Rubbish encrypted data");
        }

        var firstClosingBracketLocation = cipherText.IndexOf(']', StringComparison.OrdinalIgnoreCase);
        var cipherInformation = cipherText.Substring(1, firstClosingBracketLocation - 1).Split(".");

        int algorithm;
        if (int.TryParse(cipherInformation[0], out var foundAlgorithm))
        {
            algorithm = foundAlgorithm;
        }
        else
        {
            throw new ArgumentException("Invalid encrypted data. Cannot parse algorithm");
        }

        int keyIndex;
        if (cipherInformation.Length == 2 && int.TryParse(cipherInformation[1], out var foundKeyIndex))
        {
            keyIndex = foundKeyIndex;
        }
        else
        {
            throw new ArgumentException("Invalid encrypted data. Cannot parse key index");
        }

        var secondBracketOpeningPosition = firstClosingBracketLocation + 1;

        var secondBracketClosingPosition = cipherText.IndexOf(']', secondBracketOpeningPosition);

        var ivAndTag = cipherText.Substring(secondBracketClosingPosition + 1, secondBracketClosingPosition - 1).Split(".");

        string tagText;
        var initialVector = ivAndTag[0];
        tagText = ivAndTag[1];

        string trimmedCipherText = cipherText.Substring(secondBracketClosingPosition + 1);


        return (algorithm, keyIndex, initialVector, tagText, trimmedCipherText);
    }

    private byte[] GenerateRandomBytes(int blockSize)
    {
        return RandomNumberGenerator.GetBytes(blockSize);
    }
}