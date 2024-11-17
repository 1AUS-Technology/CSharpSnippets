namespace PerformanceEngineering.SpanUsage;

public class SpanRunner
{
    public static void Run()
    {
        var arr = new byte[10];
        Span<byte> bytes = arr;
    }
}