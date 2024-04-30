namespace ConcurrencyInC_.Synchronisation;

public class UsingAsyncLock
{
    private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);
    private int _value;

    public async Task DelayAndIncrementAsync()
    {
        await _mutex.WaitAsync();
        try
        {
            int oldValue = _value;
            await Task.Delay(TimeSpan.FromSeconds(oldValue));
            _value = oldValue + 1;
        }
        finally
        {
            _mutex.Release();
        }
    }
}