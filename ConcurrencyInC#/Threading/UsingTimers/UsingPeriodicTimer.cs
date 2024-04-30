namespace ConcurrencyInC_.Threading.UsingTimers;

public class UsingPeriodicTimer
{
    public static async Task Run()
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
        while (await timer.WaitForNextTickAsync())
        {
            Console.WriteLine("Timer triggered");
        }
    }
}