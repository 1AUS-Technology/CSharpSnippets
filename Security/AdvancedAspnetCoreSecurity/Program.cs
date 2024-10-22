namespace AdvancedAspnetCoreSecurity
{
    internal class Program
    {
        private static void Main(string[] args)
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

            Console.ReadLine();
        }
    }
}