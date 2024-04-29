namespace ConcurrencyInC_.Threading;

public class UsingCountdownEvent
{
    public static void Runner()
    {
        var countdown = new CountdownEvent(3);


        Task.Run(() =>
        {
            Console.WriteLine("Waiting for 3 seconds");
            Thread.Sleep(3000);
            Console.WriteLine("Signal 1");
            countdown.Signal();
        });

        Task.Run(() =>
        {
            Console.WriteLine("Waiting for 2 seconds");
            Thread.Sleep(2000);
            Console.WriteLine("Signal 2");
            countdown.Signal();
        });

        Task.Run(() =>
        {
            Console.WriteLine("Waiting for 1 seconds");
            Thread.Sleep(1000);
            Console.WriteLine("Signal 3");
            countdown.Signal();
        });

        // Block the code below until 3 Signal are called
        countdown.Wait();

        Console.WriteLine("Continue working after the countdown has set");
        


    }
}