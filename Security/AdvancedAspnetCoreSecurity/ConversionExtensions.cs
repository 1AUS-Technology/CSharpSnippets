using System.Text;

namespace AdvancedAspnetCoreSecurity;

public static class ConversionExtensions
{
    public static byte[] HexStringToByteArray(this string hexString)
    {
        return Enumerable.Range(0, hexString.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
            .ToArray();
    }

    public static string BytesToHex(this byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
            sb.Append(b.ToString("X2"));
        return sb.ToString();
    }
}