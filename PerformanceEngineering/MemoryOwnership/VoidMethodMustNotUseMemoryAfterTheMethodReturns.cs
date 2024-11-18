using System.Buffers;
using System.Diagnostics.Metrics;

namespace PerformanceEngineering.MemoryOwnership;

public class VoidMethodMustNotUseMemoryAfterTheMethodReturns
{
    public static void Run()
    {
        using (var owner = MemoryPool<char>.Shared.Rent())
        {
            var memory = owner.Memory;
            var span = memory.Span;
            while (true)
            {
                string? s = Console.ReadLine();

                if (s is null)
                    return;

                int value = int.Parse(s);
                if (value < 0)
                    return;

                int numCharsWritten = ToBuffer(value, span);
                Log(memory.Slice(0, numCharsWritten));
            }
        }
    }

    private static int ToBuffer(int value, Span<char> span)
    {
        string strValue = value.ToString();
        int length = strValue.Length;
        strValue.AsSpan().CopyTo(span.Slice(0, length));
        return length;
    }

    // incorrect implementation
    static void Log(ReadOnlyMemory<char> message)
    {
        // Run in background so that we don't block the main thread while performing IO.
        Task.Run(() =>
                 {
                     // WRONG: 
                     //Log violates its lease because it still attempts to use the Memory<T> instance
                     //in the background after the original method has returned. 
                     StreamWriter sw = File.AppendText(@".\input-numbers.dat");
                     sw.WriteLine(message);
                 });
    }

    // Acceptable implementation
    static Task Log2(ReadOnlyMemory<char> message)
    {
        
        // Run in the background so that we don't block the main thread while performing IO.
        return Task.Run(() => {
                            StreamWriter sw = File.AppendText(@".\input-numbers.dat");
                            sw.WriteLine(message);
                            sw.Flush();
                        });
    }

    // or you can convert the message to the string
    //string defensiveCopy = message.ToString();
}