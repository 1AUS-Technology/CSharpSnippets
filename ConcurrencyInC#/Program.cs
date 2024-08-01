﻿using ConcurrencyInC_.Synchronisation;
using ConcurrencyInC_.Threading;
using ConcurrencyInC_.Threading.UsingReaderWriterLockSlim;
using ConcurrencyInC_.Threading.UsingTimers;

namespace ConcurrencyInC_;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            Boolean.Parse("AAA");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
        //await  DataFlowBasics.LinkBlocks();
        //ReactiveBasics.Runner();
        //await UseCollections.Runner();

        //await Cancellation.Runner();

        //UsingCountdownEvent.Runner();
        //UsingLockProvider.Run();
        //await SynchronisationRunner.Run();
        //await UsingAsyncStream.Run();

        //UsingSemaphore.Run();

        //UsingAutoResetEvent.Run();

        //UsingBarrier.Run();
        //await UsingPeriodicTimer.Run();
        ThreadPool.GetMaxThreads(out var workerThreads, out var completionPortThreads);

        Console.WriteLine($"Max worker threads {workerThreads} and completion threads {completionPortThreads}");

        var task = PerformanceClient.DoWork();

        await task;
        Console.ReadLine();
    }
}