using System.Security.Cryptography;
using System.Text;
using IdentityExample.CustomSecurity.CustomStore;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity;

public class SimplePasswordHasher : IPasswordHasher<AppUser>
{
    public string HashPassword(AppUser user, string password)
    {
        byte[] key = Encoding.UTF32.GetBytes(user.Id);
        HMACSHA512 hashAlgo = new HMACSHA512(key);
        byte[] hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hash);
    }

    public PasswordVerificationResult VerifyHashedPassword(AppUser user, string hashedPassword, string providedPassword)
    {
        return HashPassword(user, providedPassword).Equals(hashedPassword)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }
}