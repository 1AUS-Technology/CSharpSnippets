using System;
using System.Buffers;
using System.Diagnostics;

namespace PerformanceEngineering.MemoryOwnership;

public class MemoryOwnerUsage
{
    public static void Run()
    {
        IMemoryOwner<char> owner = MemoryPool<char>.Shared.Rent(1000);
        try
        {
            

            Console.WriteLine("enter a number");
            string? s = Console.ReadLine();

            if (s is null)
                return;

            var value = int.Parse(s);

            var memory = owner.Memory;
            WriteInt32ToBuffer(value, memory);

            DisplayBufferToConsole(owner.Memory.Slice(0, value.ToString().Length));
        }
        finally
        {
            owner.Dispose();
        }
    }

    private static void DisplayBufferToConsole(Memory<char> slice)
    {
        Console.WriteLine($"Contents of the buffer: '{slice}'");
    }

    private static void WriteInt32ToBuffer(int value, Memory<char> memory)
    {
        var valString = value.ToString();

        var span = memory.Span;
        for (int i = 0; i < valString.Length; i++)
        {
            span[i] = valString[i];
        }
    }
}