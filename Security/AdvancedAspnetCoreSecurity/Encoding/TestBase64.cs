using System.Text.Encodings.Web;

namespace AdvancedAspnetCoreSecurity.Encoding;

public class TestBase64
{
    public static void Run()
    {
        var a = new byte[] { 0 };
        Convert.ToBase64String(a).Dump();
        
        
    }
}