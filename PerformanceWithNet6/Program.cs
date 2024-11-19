using BenchmarkDotNet.Running;
using PerformanceWithNet6.BenchmarkExamples;
using PerformanceWithNet6.MemoryAllocation;

namespace PerformanceWithNet6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<UsingDynamicType>();
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
