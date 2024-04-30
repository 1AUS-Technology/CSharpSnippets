namespace ConcurrencyInC_.Threading;

public static class WaitAsyncExtensions
{
    public static async Task<IDisposable> EnterAsync(this SemaphoreSlim ss)
    {
        await ss.WaitAsync().ConfigureAwait(false);
        // Do something and then release the lock when finish
        return Disposable.Create(() => ss.Release());
    }
}