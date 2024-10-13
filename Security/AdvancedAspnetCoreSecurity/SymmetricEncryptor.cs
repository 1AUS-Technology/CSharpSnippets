using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace AdvancedAspnetCoreSecurity;

public class SymmetricEncryptor
{
    public enum EncryptionAlgorithm
    {
        AES128 = 1
    }
    private EncryptionAlgorithm _defaultAlgorithm = EncryptionAlgorithm.AES128;
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
                throw new NotImplementedException($"Cannot find block size for{ algorithm.ToString() } algorithm");
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

    private string EncryptAES(string plainText, string key, EncryptionAlgorithm algorithm, int keyIndex)
    {
        var keyBytes = key.HexStringToByteArray();
        var initialVector = GenerateRandomBytes(GetBlockSizeInBytes(algorithm));


        byte[] encryptedBytes = [];


        var encryptedText = encryptedBytes.BytesToHex();
        var ivText = initialVector.BytesToHex();

        // concatenate all key and IV information for decryption
        return $"[{(int)algorithm},{keyIndex}]{ivText}{encryptedText}";
    }

    private byte[] GenerateRandomBytes(int blockSize)
    {
        return RandomNumberGenerator.GetBytes(blockSize);
    }
}