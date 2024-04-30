using System;

namespace ConcurrencyInC_.Synchronisation;

public class LoggingUsingTheSameId
{
    private AsyncLocalGuidStack _operationId = new AsyncLocalGuidStack();

    public async Task LengthyOperation()
    {
        var guid = Guid.NewGuid();
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} generates GUID {guid}");
        using (_operationId.Push(guid))
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            await ExecuteChildOperation1();
            await Task.Delay(TimeSpan.FromMilliseconds(20));
            await ExecuteChildOperation2();
        }
    }

    private async Task ExecuteChildOperation2()
    {
        Console.WriteLine($"Child operation 2 on thread {Thread.CurrentThread.ManagedThreadId} use GUID {_operationId.PeekCurrentValue}");
    }

    private async Task ExecuteChildOperation1()
    {
        Console.WriteLine($"Child operation 1 on thread {Thread.CurrentThread.ManagedThreadId} use GUID {_operationId.PeekCurrentValue}");
    }

    public async Task PrintStack()
    {
        _operationId.PrintStack();
    }
}