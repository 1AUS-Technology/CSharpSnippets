using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace PerformanceEngineering.ArrayPoolUsage;


public class PooledArray(int size) : IDisposable
{
    public byte[] Data { get; private set; } = ObjectPool.BytePool.Rent(size);

    public void Dispose()
    {
        ObjectPool.BytePool.Return(Data);
    }
}