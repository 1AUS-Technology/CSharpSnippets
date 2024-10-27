using System.Text;
using AdvancedAspnetCoreSecurity.Issues;

namespace AdvancedAspnetCoreSecurity
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            (string encryptionKey, string initialVector) = ReadKeyInformationFromConfigurationFile();

            string plainText = "mypassword";
            byte[] keyBytes = Convert.FromBase64String(encryptionKey);
            byte[] ivBytes = Convert.FromBase64String(initialVector);

            byte[] encryptedBytes = SymmetricEncryptionWithMistakes.Encrypt(plainText,keyBytes , ivBytes);

            string decryptedText = SymmetricEncryptionWithMistakes.Decrypt(encryptedBytes, keyBytes, ivBytes);
            
            Console.WriteLine($"Plain text: {plainText}");
            Console.WriteLine($"Encrypted (HEX): { encryptedBytes.BytesToHex()}");
            Console.WriteLine($"Decrypted text: {decryptedText}");

            Console.ReadLine();
        }

        private static (string encryptionKey, string initialVector) ReadKeyInformationFromConfigurationFile()
        {
            return ("vbq2dvHswgEa2N2fSnqt2Ri3C/JOhj4WHNTsq2FetOE=", "2nWjCsQjC65l+ggTcnZ8iA==");
        }

        private static void RunUpdatedEncryption()
        {
            var plainText = "This is a secret message";

            ISecretStore secretStore = new EnvironmentVariableSecretStore();
            ISymmetricEncryptor encryptor = new SymmetricEncryptor(secretStore);

            string keyName = "test_encryption_key_name";
            int keyIndex = 1;

            string encryptedText = encryptor.Encrypt(plainText, keyName, keyIndex, EncryptionAlgorithm.Aes128);

            Console.WriteLine($"Encrypted text: {encryptedText} ");

            string decryptedText = encryptor.Decrypt(encryptedText, keyName);

            Console.WriteLine($"Original text:  {plainText,-30}");
            Console.WriteLine($"Decrypted text: {decryptedText,-30}");
        }
    }
}