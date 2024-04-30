namespace ConcurrencyInC_.Synchronisation;

public class InitialiseOnce
{
    private static int _expensiveResource;
    private readonly Lazy<Task<int>> sharedInteger = new Lazy<Task<int>>(async () =>
    {
        await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
        return _expensiveResource++;
    });

    async Task GetSharedResource()
    {
        var sharedValue = await sharedInteger.Value;
    }
}