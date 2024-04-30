namespace ConcurrencyInC_.Threading;

public class Disposable: IDisposable
{
    private Action? _onDispose;
    public static Disposable Create(Action onDispose) => new(onDispose);
    private Disposable(Action onDispose) => _onDispose = onDispose;
    public void Dispose()
    {
        _onDispose?.Invoke();
        // ensure that it can't execute the second time
        _onDispose = null;
    }
}