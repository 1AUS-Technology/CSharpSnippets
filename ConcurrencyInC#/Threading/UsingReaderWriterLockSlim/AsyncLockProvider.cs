using Nito.AsyncEx;
using System.Threading;

namespace ConcurrencyInC_.Threading.UsingReaderWriterLockSlim;

public class AsyncLockProvider
{
    private AsyncReaderWriterLock Schedule { get; set; } = new();

    public Task<IDisposable> RequestScheduleReaderLockAsync(CancellationToken cancellationToken = default)
    {
        return Schedule.ReaderLockAsync(cancellationToken);
    }


    public Task<IDisposable> RequestScheduleWriterLockAsync(CancellationToken cancellationToken = default)
    {
        return Schedule.WriterLockAsync(cancellationToken);
    }

}

