﻿using ConcurrencyInC_.Threading;

namespace ConcurrencyInC_;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //await  DataFlowBasics.LinkBlocks();
        //ReactiveBasics.Runner();
        //await UseCollections.Runner();

        //await Cancellation.Runner();

        UsingCountdownEvent.Runner();

        Console.ReadLine();
    }
}