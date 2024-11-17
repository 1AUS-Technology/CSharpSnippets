namespace PerformanceEngineering.UnitTests;

public class MemoryUsage
{
    static async Task<int> CheckSumReadAsync(Memory<byte> buffer, Stream stream)
    {
        int bytesRead = await stream.ReadAsync(buffer);
        return Checksum(buffer.Span.Slice(0, bytesRead));
    }

    static int Checksum(Span<byte> buffer)
    {
        return 0;
    }
}