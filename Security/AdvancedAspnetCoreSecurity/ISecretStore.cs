namespace AdvancedAspnetCoreSecurity;

public interface ISecretStore
{
    /// <summary>
    /// The keyIndex is there so you can upgrade your keys periodically (which is a process called key rotation) without needing to
    /// go through a data migration in which all of your encrypted values are decrypted with the old key and encrypted with the new one
    /// </summary>
    /// <param name="keyName"></param>
    /// <param name="keyIndex"></param>
    /// <returns></returns>
    string GetKey(string keyName, int keyIndex);
    string GetSalt(string saltName);
}