namespace ConcurrencyInC_.Threading;

public class UsingSemaphore
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(3);
    public static void Run()
    {
        

        for (int i = 1; i < 8; i++)
        {
            new Thread(EnterTheClub).Start( i);
        }

        Console.ReadLine();
    }

    private static void EnterTheClub(object? id)
    {
        Console.WriteLine($"{id} wants to enter");
        semaphore.Wait();
        Console.WriteLine($"{id} is in");
        Thread.Sleep(1000* (int)id);
        Console.WriteLine($"{id} is leaving");
        semaphore.Release();
    }
}