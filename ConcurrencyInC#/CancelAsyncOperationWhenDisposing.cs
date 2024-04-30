namespace ConcurrencyInC_;

public class CancelAsyncOperationWhenDisposing: IDisposable
{
    private readonly CancellationTokenSource cts = new CancellationTokenSource();


    public async Task<int> CalculateValueAsync(CancellationToken cancellationToken)
    {
        //Combine user cancellation token source
        using CancellationTokenSource combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token);
        await Task.Delay(TimeSpan.FromSeconds(2), combinedTokenSource.Token);
        return 100;
    }

    private bool IsDisposed { get; set; }
    public void Dispose()
    {
        if(IsDisposed) return;
        cts.Cancel();
        cts.Dispose();
        IsDisposed = true;
    }
}