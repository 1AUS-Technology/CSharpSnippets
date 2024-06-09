using System.Diagnostics;

namespace ConcurrencyInC_.Threading;

public class PerformanceMeasurer
{
    public IDisposable StartMeasuring(Action<double> action)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        return Disposable.Create(() =>
        {
            stopWatch.Stop();
            action(stopWatch.ElapsedMilliseconds);
        });
    }
}

public class PerformanceClient
{
    static PerformanceMeasurer  measurer= new PerformanceMeasurer();
    
    public static async Task DoWork()
    {
        using (measurer.StartMeasuring(milliseconds=> Console.WriteLine($"The action took {milliseconds} ms")))
        {
            Console.WriteLine("Doing some work");
           await Task.Delay(3000);
        }
        
    }
}