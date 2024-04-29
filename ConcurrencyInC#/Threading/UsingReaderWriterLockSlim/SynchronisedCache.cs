namespace ConcurrencyInC_.Threading.UsingReaderWriterLockSlim;

public class SynchronisedCache : IDisposable
{
    private readonly Dictionary<int, string> cache = new();
    private readonly ReaderWriterLockSlim cacheLock = new();

    public int Count => cache.Count;

    public void Dispose()
    {
        cacheLock.Dispose();
    }

    public string Read(int key)
    {
        cacheLock.EnterReadLock();
        try
        {
            return cache[key];
        }
        finally
        {
            cacheLock.ExitReadLock();
        }
    }

    public void Add(int key, string value)
    {
        cacheLock.EnterWriteLock();
        try
        {
            cache[key] = value;
        }
        finally
        {
            cacheLock.ExitWriteLock();
        }
    }

    public bool AddWithTimeout(int key, string value, TimeSpan timeout)
    {
        if (cacheLock.TryEnterWriteLock(timeout))
        {
            try
            {
                cache[key] = value;
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }

            return true;
        }

        return false;
    }

    public void Delete(int key)
    {
        cacheLock.EnterWriteLock();
        try
        {
            cache.Remove(key);
        }
        finally
        {
            cacheLock.ExitWriteLock();
        }
    }

    public AddOrUpdateStatus AddOrUpdate(int key, string value)
    {
        cacheLock.EnterUpgradeableReadLock();
        try
        {
            if (cache.TryGetValue(key, out var result))
            {
                if (result == value)
                {
                    return AddOrUpdateStatus.Unchanged;
                }

                return UpdateItem();
            }

            return AddItem();
        }
        finally
        {
            cacheLock.ExitUpgradeableReadLock();
        }

        AddOrUpdateStatus AddItem()
        {
            try
            {
                cacheLock.EnterWriteLock();
                cache[key] = value;
                return AddOrUpdateStatus.Added;
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        AddOrUpdateStatus UpdateItem()
        {
            cacheLock.EnterWriteLock();
            try
            {
                cache[key] = value;

                return AddOrUpdateStatus.Updated;
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
    }
}

public enum AddOrUpdateStatus
{
    Added,
    Updated,
    Unchanged
}