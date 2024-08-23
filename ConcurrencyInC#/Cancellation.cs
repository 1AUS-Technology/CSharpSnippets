namespace ConcurrencyInC_;

public static class Cancellation
{
    public static async Task Runner()
    {
        //CancelTask();
        //await IssueCancellationRequest();
        await CancelAfterTimeout();
        //await CallWaitUntilCompletionOrCancellation();
    }

    private static void CancelTask()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;

        Task.Run(() =>
        {
            while (!token.IsCancellationRequested)
            {
                Console.WriteLine("Task is running");
                Thread.Sleep(1000);
            }

            Console.WriteLine("Task was cancelled");
        }, token);

        Console.WriteLine("Press any key to cancel the task");
        Console.ReadKey();
        cts.Cancel();
    }

    private static async Task IssueCancellationRequest()
    {
        using var cts = new CancellationTokenSource();

        // Start a task that will run until cancelled
        Task.Run(async () => await RunCancellationTask(cts.Token));

        await Task.Delay(2000);

        Console.WriteLine("Request to cancel the task");
        await cts.CancelAsync();
    }

    private static async Task RunCancellationTask(CancellationToken token)
    {
        var task = CancellableTask(token);
        // There will be 3 possibilities when a task is cancelled
        try
        {
            await task;

            //1. Task completes successfully
            Console.WriteLine("Task Completed successfully");
        }
        catch (OperationCanceledException)
        {
            //2. Task was cancelled
            Console.WriteLine("Task was cancelled");
        }
        catch (Exception exception)
        {
            //3. Task failed with an exception
            Console.WriteLine("Task Failed with an exception");
            Console.WriteLine(exception);
        }
    }

    private static async Task CancellableTask(CancellationToken token)
    {
        var iterations = 0;
        while (!token.IsCancellationRequested)
        {
            Console.WriteLine("Working hard");
            if (iterations++ == 5)
            {
                break;
            }

            await Task.Delay(1000);
        }

        token.ThrowIfCancellationRequested();
        Console.WriteLine("Task Completed successfully");

    }

    private static async Task CancelAfterTimeout()
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        Console.WriteLine("Starting at " + DateTime.Now);
        try
        {

            cts.Token.Register(() => Console.WriteLine($"Token is cancelled at {DateTime.Now}"));
            // this task will be cancelled after 3 seconds
            await CancellableTask(cts.Token);

            
            Console.WriteLine("Task finished");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Task was cancelled");
        }
    }

    private static async Task<HttpResponseMessage> LinkCancellationTokens(HttpClient client, string url, CancellationToken cancellationToken)
    {
        using CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(2));
        CancellationToken combinedToken = cts.Token;

        return await client.GetAsync(url, combinedToken);

    }

    private static async Task CallWaitUntilCompletionOrCancellation()
    {
        var cts = new CancellationTokenSource();

        try
        {

            cts.CancelAfter(TimeSpan.FromSeconds(3));

            var task =  CancellableTask(cts.Token);

            await task.WaitUntilCompletionOrCancellation(cts.Token);

            Console.WriteLine("Task finished");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Task was cancelled");
        }

    }

    public static async Task WaitUntilCompletionOrCancellation(this Task asyncOp, CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<bool>();
        await using (cancellationToken.Register(() => tcs.TrySetCanceled()))
        {
           await Task.WhenAny(asyncOp, tcs.Task);
           cancellationToken.ThrowIfCancellationRequested();
        }
    }
}