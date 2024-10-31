using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.CustomSecurity.CustomStore;

public abstract class SimpleTokenGenerator : IUserTwoFactorTokenProvider<AppUser>
{
    protected virtual int CodeLength { get; } = 6;

    public Task<string> GenerateAsync(string purpose, UserManager<AppUser> manager, AppUser user)
    {
        return Task.FromResult(GenerateCode(purpose, user));
    }

    public Task<bool> ValidateAsync(string purpose, string token, UserManager<AppUser> manager, AppUser user)
    {
        var generatedCode = GenerateCode(purpose, user);
        return Task.FromResult(generatedCode == token);
    }

    public virtual Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser> manager, AppUser user)
    {
        return Task.FromResult(manager.SupportsUserSecurityStamp);
    }

    private string GenerateCode(string purpose, AppUser user)
    {
        HMACSHA1 hashAlgorithm =
            new HMACSHA1(Encoding.UTF8.GetBytes(user.SecurityStamp));
        byte[] purposeAndSecurityStamp = Encoding.UTF8.GetBytes($"{purpose}{user.SecurityStamp}");
        byte[] hashCode = hashAlgorithm.ComputeHash(purposeAndSecurityStamp);
        return BitConverter.ToString(hashCode[^CodeLength..]).Replace("-", "");
    }
}

public class EmailConfirmationTokenGenerator : SimpleTokenGenerator
{
    protected override int CodeLength => 12;

    public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser> manager, AppUser user)
    {
        return await base.CanGenerateTwoFactorTokenAsync(manager, user) &&
               !string.IsNullOrEmpty(user.EmailAddress) &&
               !user.EmailAddressConfirmed;
    }
}

public class PhoneConfirmationTokenGenerator : SimpleTokenGenerator
{
    protected override int CodeLength => 3;

    public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser> manager, AppUser user)
    {
        return await base.CanGenerateTwoFactorTokenAsync(manager, user)
               && !string.IsNullOrEmpty(user.PhoneNumber)
               && !user.PhoneNumberConfirmed;
    }
}