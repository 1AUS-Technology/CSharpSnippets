using System.Security.Cryptography;

namespace SecureCoding.NetSecurity.FileSecurity;

public class EncryptAndDecryptFile
{
    public void EncryptFile(FileInfo file, string folderPath, RSACryptoServiceProvider keyEncryptor)
    {
        // Create instance of Aes for
        // symmetric encryption of the data.
        Aes aes = Aes.Create();
        ICryptoTransform transform = aes.CreateEncryptor();

        // Use RSACryptoServiceProvider to
        // encrypt the AES key.
        // rsa is previously instantiated:
        //    rsa = new RSACryptoServiceProvider(cspp);
        byte[] keyEncrypted = keyEncryptor.Encrypt(aes.Key, false);

        // Create byte arrays to contain
        // the length values of the key and IV.
        int lKey = keyEncrypted.Length;
        byte[] LenK = BitConverter.GetBytes(lKey);
        int lIV = aes.IV.Length;
        byte[] LenIV = BitConverter.GetBytes(lIV);

        // Write the following to the FileStream
        // for the encrypted file (outFs):
        // - length of the key
        // - length of the IV
        // - encrypted key
        // - the IV
        // - the encrypted cipher content

        // Change the file's extension to ".enc"
        string outFile =
            Path.Combine(folderPath, Path.ChangeExtension(file.Name, ".enc"));

        using (var outFs = new FileStream(outFile, FileMode.Create))
        {
            outFs.Write(LenK, 0, 4);
            outFs.Write(LenIV, 0, 4);
            outFs.Write(keyEncrypted, 0, lKey);
            outFs.Write(aes.IV, 0, lIV);

            // Now write the cipher text using
            // a CryptoStream for encrypting.
            using (var outStreamEncrypted =
                new CryptoStream(outFs, transform, CryptoStreamMode.Write))
            {
                // By encrypting a chunk at
                // a time, you can save memory
                // and accommodate large files.
                int count = 0;
                int offset = 0;

                // blockSizeBytes can be any arbitrary size.
                int blockSizeBytes = aes.BlockSize / 8;
                byte[] data = new byte[blockSizeBytes];
                int bytesRead = 0;

                using (var inFs = new FileStream(file.FullName, FileMode.Open))
                {
                    do
                    {
                        count = inFs.Read(data, 0, blockSizeBytes);
                        offset += count;
                        outStreamEncrypted.Write(data, 0, count);
                        bytesRead += blockSizeBytes;
                    } while (count > 0);
                }
                outStreamEncrypted.FlushFinalBlock();
            }
        }
    }

    public void DecryptFile(FileInfo file, string decryptionFolderPath, RSACryptoServiceProvider keyDecryptor)
    {
        // Create instance of Aes for
        // symmetric decryption of the data.
        Aes aes = Aes.Create();

        // Create byte arrays to get the length of
        // the encrypted key and IV.
        // These values were stored as 4 bytes each
        // at the beginning of the encrypted package.
        byte[] LenK = new byte[4];
        byte[] LenIV = new byte[4];

        // Construct the file name for the decrypted file.
        string outFile =
            Path.ChangeExtension(file.FullName.Replace("Encrypt", "Decrypt"), ".txt");

        // Use FileStream objects to read the encrypted
        // file (inFs) and save the decrypted file (outFs).
        using (var inFs = new FileStream(file.FullName, FileMode.Open))
        {
            inFs.Seek(0, SeekOrigin.Begin);
            inFs.Read(LenK, 0, 3);
            inFs.Seek(4, SeekOrigin.Begin);
            inFs.Read(LenIV, 0, 3);

            // Convert the lengths to integer values.
            int lenK = BitConverter.ToInt32(LenK, 0);
            int lenIV = BitConverter.ToInt32(LenIV, 0);

            // Determine the start position of
            // the cipher text (startC)
            // and its length(lenC).
            int startC = lenK + lenIV + 8;
            int lenC = (int)inFs.Length - startC;

            // Create the byte arrays for
            // the encrypted Aes key,
            // the IV, and the cipher text.
            byte[] KeyEncrypted = new byte[lenK];
            byte[] IV = new byte[lenIV];

            // Extract the key and IV
            // starting from index 8
            // after the length values.
            inFs.Seek(8, SeekOrigin.Begin);
            inFs.Read(KeyEncrypted, 0, lenK);
            inFs.Seek(8 + lenK, SeekOrigin.Begin);
            inFs.Read(IV, 0, lenIV);

            Directory.CreateDirectory(decryptionFolderPath);
            // Use RSACryptoServiceProvider
            // to decrypt the AES key.
            byte[] KeyDecrypted = keyDecryptor.Decrypt(KeyEncrypted, false);

            // Decrypt the key.
            ICryptoTransform decryptor = aes.CreateDecryptor(KeyDecrypted, IV);

            // Decrypt the cipher text from
            // from the FileSteam of the encrypted
            // file (inFs) into the FileStream
            // for the decrypted file (outFs).
            using (var outFileStream = new FileStream(outFile, FileMode.Create))
            {
                int count = 0;
                int offset = 0;

                // blockSizeBytes can be any arbitrary size.
                int blockSizeBytes = aes.BlockSize / 8;
                byte[] data = new byte[blockSizeBytes];

                // By decrypting a chunk a time,
                // you can save memory and
                // accommodate large files.

                // Start at the beginning
                // of the cipher text.
                inFs.Seek(startC, SeekOrigin.Begin);
                using (var outStreamDecrypted = new CryptoStream(outFileStream, decryptor, CryptoStreamMode.Write))
                {
                    do
                    {
                        count = inFs.Read(data, 0, blockSizeBytes);
                        offset += count;
                        outStreamDecrypted.Write(data, 0, count);
                    } while (count > 0);

                    outStreamDecrypted.FlushFinalBlock();
                }
            }
        }
    }
}