using System.Net;
using System.Threading;

namespace ConcurrencyInC_.Threading;

public class UsingSemaphore
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(3);
    public static async Task Run()
    {
        //RunBasicSemaphore();

        var bytes = await DownloadWithSemaphoreAsync("https://afr.com");

        Console.WriteLine("Download finished");

        Console.ReadLine();
    }

    private static void RunBasicSemaphore()
    {
        for (int i = 1; i < 8; i++)
        {
            new Thread(EnterTheClub).Start( i);
        }
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


    private static async Task<byte[]> DownloadWithSemaphoreAsync(string uri)
    {
        using (await semaphore.EnterAsync())
            return await new WebClient().DownloadDataTaskAsync(uri);
    }
}