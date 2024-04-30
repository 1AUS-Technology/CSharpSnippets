namespace ConcurrencyInC_.Synchronisation;

public class SignalAnotherThread
{
    private readonly ManualResetEventSlim _signalling = new ManualResetEventSlim();
    private int _counter = 0;

    public int WaitForSignal()
    {
        _signalling.Wait();
        return _counter;
    }

    public void SignalToGo()
    {
        _counter++;
        _signalling.Set();
        _signalling.Reset();
    }
}