using BenchmarkDotNet.Running;
using PerformanceWithNet6.MemoryAllocation;

namespace PerformanceWithNet6
{
    internal class Program
    {
        static void Main(string[] args)
        {

            BenchmarkRunner.Run<UsingDynamicType>();
        }
    }
}
