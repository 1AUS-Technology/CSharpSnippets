using System.Security.Cryptography;

namespace SecureCoding.NetSecurity;

public class DecryptData
{
    public static async Task Run()
    {
       await SymmetricDecryption();
    }

    private static  async Task SymmetricDecryption()
    {
        try
        {
            byte[] decryptionKey =
            {
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
            };

            using (FileStream fileStream = new("TestData.txt", FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.IV.Length];
                    int ivBytesToRead = aes.IV.Length;

                    int numberOfBytesAlreadyRead = 0;
                    while (ivBytesToRead > 0)
                    {
                        int n = fileStream.Read(iv, numberOfBytesAlreadyRead, ivBytesToRead);
                        if (n == 0)
                        {
                            break;
                        }

                        numberOfBytesAlreadyRead += n;
                        ivBytesToRead -= n;
                    }

                   

                    using (CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(decryptionKey, iv),
                               CryptoStreamMode.Read))
                    {
                        // By default, the StreamReader uses UTF-8 encoding.
                        // To change the text encoding, pass the desired encoding as the second parameter.
                        // For example, new StreamReader(cryptoStream, Encoding.Unicode).
                        using (StreamReader decryptReader = new(cryptoStream))
                        {
                            string decryptedMessage = await decryptReader.ReadToEndAsync();
                            Console.WriteLine($"The decrypted original message: {decryptedMessage}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"The decryption failed. {ex}");
        }
    }
}