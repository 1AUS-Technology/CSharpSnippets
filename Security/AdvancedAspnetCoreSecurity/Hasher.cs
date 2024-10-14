using System.Security.Cryptography;
using System.Text;

namespace AdvancedAspnetCoreSecurity;

public class Hasher
{
    public string HashSHA512(string plainText, string salt,
        bool saveSaltInResult)
    {
        var fullText = string.Concat(plainText, salt);
        var data = Encoding.UTF8.GetBytes(fullText);
        using (SHA512 sha = new SHA512Managed())
        {
            var hashBytes = sha.ComputeHash(data);
            var asString = hashBytes.BytesToHex();
            return saveSaltInResult ? $"[{(int)HashAlgorithm.SHA512}]{salt}{asString}" : $"[{(int)HashAlgorithm.SHA512}]{asString}";
        }
    }

    
}

public interface IHasher
{
    string CreateHash(string plainText,
        HashAlgorithm algorithm);
    string CreateHash(string plainText, string salt,
        HashAlgorithm algorithm);
    bool MatchesHash(string plainText, string hash);
}