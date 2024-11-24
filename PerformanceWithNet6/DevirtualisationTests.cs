using BenchmarkDotNet.Attributes;

namespace PerformanceWithNet6;

public class DevirtualisationTests
{
    internal interface IValueProducer
    {
        int GetValue();
    }

    class Producer42 : IValueProducer
    {
        public int GetValue() => 42;
    }

    private IValueProducer _valueProducer;
    private int _factor = 2;

    [GlobalSetup]
    public void Setup() => _valueProducer = new Producer42();

    [Benchmark]
    public int GetValue() => _valueProducer.GetValue() * _factor;
}