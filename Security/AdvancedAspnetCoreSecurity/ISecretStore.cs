namespace AdvancedAspnetCoreSecurity;

public interface ISecretStore
{
    string GetKey(string keyName, int keyIndex);
}

public class EnvironmentVariableSecretStore : ISecretStore
{
    public string GetKey(string keyName, int keyIndex)
    {
        var envVarName = $"{keyName}_{keyIndex}";
        return GetEnvironmentVariable(envVarName);
    }


    private string GetEnvironmentVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Environment variable '{name}' not found or empty.");
        }

        return value;
    }
}