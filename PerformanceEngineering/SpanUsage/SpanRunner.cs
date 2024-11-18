namespace PerformanceEngineering.SpanUsage;

public class SpanRunner
{
    public static void Run()
    {
        var arr = new byte[10];
        Span<byte> bytes = arr;

        // Allocate 100 bytes on the stack

        byte data = 0;
        Span<byte> stackSpan = stackalloc byte[100];
        for (int ctr = 0; ctr < stackSpan.Length; ctr++)
            stackSpan[ctr] = data++;

        int stackSum = 0;
        foreach (var value in stackSpan)
            stackSum += value;

        Console.WriteLine($"The sum is {stackSum}");
    }
}