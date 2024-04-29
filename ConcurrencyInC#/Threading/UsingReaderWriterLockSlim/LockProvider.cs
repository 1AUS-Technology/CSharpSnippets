using Nito.AsyncEx;
using System.Threading;

namespace ConcurrencyInC_.Threading.UsingReaderWriterLockSlim;

public class LockProvider
{
    public AsyncReaderWriterLock Lock { get; set; } = new();
    public string LocksHeld => Lock?.ToString();


    public Task<IDisposable> RequestReaderLockAsync(CancellationToken cancellationToken = default)
    {
        return Lock.ReaderLockAsync(cancellationToken);
    }


    public Task<IDisposable> RequestWriterLockAsync(CancellationToken cancellationToken = default)
    {
        return Lock.WriterLockAsync(cancellationToken);
    }

}

