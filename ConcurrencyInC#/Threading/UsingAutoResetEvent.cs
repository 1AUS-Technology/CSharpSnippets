namespace ConcurrencyInC_.Threading;

public class UsingAutoResetEvent
{
    static EventWaitHandle _waitHandle = new AutoResetEvent(false);

   public static void Run()
    {
        new Thread(Waiter).Start();
        Thread.Sleep(1000);                  // Pause for a second...
        Console.WriteLine("Signalling");
        _waitHandle.Set();                    // Wake up the Waiter.
    }

    static void Waiter()
    {
        Console.WriteLine("Waiting...");
        _waitHandle.WaitOne();                // Wait for notification
        Console.WriteLine("Notified");
    }
}