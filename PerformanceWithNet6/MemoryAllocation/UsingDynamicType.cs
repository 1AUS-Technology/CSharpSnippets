using BenchmarkDotNet.Attributes;

namespace PerformanceWithNet6.MemoryAllocation;

[DisassemblyDiagnoser(maxDepth: 0)]
[HideColumns("Job", "Error", "StdDev", "Median", "RatioSD")]
public class UsingDynamicType
{
    dynamic _dynamicType;

    [Benchmark]
    public void MeasureVarUsage()
    {
        var x = 3.14159;
    }

    [Benchmark]
    public void MeasureVarDynamicUsage()
    {
        var x = (dynamic)3.14159;
    }


    [Benchmark]
    public void MeasureTypeTypeUsage()
    {
        double x = 3.14159;
    }

}