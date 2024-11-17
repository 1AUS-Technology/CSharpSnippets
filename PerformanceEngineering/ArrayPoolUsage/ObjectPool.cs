using System.Buffers;

namespace PerformanceEngineering.ArrayPoolUsage;

public static class ObjectPool
{
    internal static ArrayPool<byte> BytePool = ArrayPool<byte>.Create();
}