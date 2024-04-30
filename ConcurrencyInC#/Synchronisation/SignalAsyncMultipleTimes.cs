using Nito.AsyncEx;

namespace ConcurrencyInC_.Synchronisation;

public class SignalAsyncMultipleTimes
{
    private readonly AsyncManualResetEvent _connected = new AsyncManualResetEvent(false);

    public async Task WaitForConnectedAsync()
    {
        await _connected.WaitAsync();
    }

    public void ConnectionChanged(bool connected)
    {
        if (connected)
        {
            _connected.Set();
        }
        else
        {
            _connected.Reset();
        }
    }
}