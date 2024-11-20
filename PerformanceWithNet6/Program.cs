using BenchmarkDotNet.Running;

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
