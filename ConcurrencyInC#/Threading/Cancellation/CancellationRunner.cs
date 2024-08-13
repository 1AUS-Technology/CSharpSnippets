namespace ConcurrencyInC_.Threading.Cancellation;

public class CancellationRunner
{
    public static void Run()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        var token = cts.Token;

        
        var cancellableObject = new CancelableObject("1");
        token.Register(() => cancellableObject.Cancel());

        ThreadPool.QueueUserWorkItem(DoSomeWok, cts.Token);


        Thread.Sleep(2500);

        cts.Cancel();
        Console.WriteLine("Cancellation set in token source...");
        Thread.Sleep(2500);
        // Cancellation should have happened, so call Dispose.
        cts.Dispose();
    }

    private static void DoSomeWok(object? state)
    {
        if (state is null)
            return;

        CancellationToken token = (CancellationToken)state;

        for (int i = 0; i < 100000; i++)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("In iteration {0}, cancellation has been requested...",
                    i + 1);
                // Perform cleanup if necessary.
                //...
                // Terminate the operation.
                break;
            }
            // Simulate some work.
            Thread.SpinWait(500000);
        }
    }


    class CancelableObject(string id)
    {
        public string id = id;

        public void Cancel()
        {
            Console.WriteLine("Object {0} Cancel callback", id);
            // Perform object cancellation here.
        }
    }

  
}