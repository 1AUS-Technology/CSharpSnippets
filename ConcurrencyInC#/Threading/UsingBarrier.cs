namespace ConcurrencyInC_.Threading;

public class UsingBarrier
{
    // wait 3 threads to call SignalAndWait() before continues 
    private static Barrier barrier = new Barrier(3);
    public static void Run()
    {
        for (int i = 0; i < 10; i++)
        {
            new Thread(Speak).Start(i);
        }
    }

    private static void Speak(object? id)
    {
        for (int i = 1; i < 5; i++)
        {
            Console.WriteLine($"{id} is speaking {i} times");
            Thread.Sleep(TimeSpan.FromSeconds((int)id));
            barrier.SignalAndWait();
            
        }
    }
}