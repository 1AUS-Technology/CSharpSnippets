namespace ConcurrencyInC_.Threading;

public class UsingAsyncStream
{
   static async IAsyncEnumerable<int> RangeAsync(
        int start, int count, int delay)
    {
        for (int i = start; i < start + count; i++)
        {
            await Task.Delay(delay);
            yield return i;
        }
    }

    public static async Task Run()
    {
        await foreach (var number in RangeAsync(0, 10, 1500))
        {
            Console.WriteLine(number);
        }
    }
}