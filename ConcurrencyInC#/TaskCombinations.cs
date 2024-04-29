using System.Collections.Concurrent;

namespace ConcurrencyInC_;

public class TaskCombinations
{
    public static async Task<T?> RetryOnFault<T>(Func<Task<T>> function, int maxTries)
    {
        for (int i = 0; i < maxTries; i++)
        {
            try
            {
                return await function().ConfigureAwait(false);
            }
            catch
            {
                if (i == maxTries - 1) throw;
            }
        }

        return default;
    }

    public static async Task<T> NeedOnlyOne<T>(params Func<CancellationToken, Task<T>>[] functions)
    {
        var cts = new CancellationTokenSource();
        var tasks = (from function in functions select function(cts.Token)).ToArray();

        Task<T> completed = await Task.WhenAny(tasks).ConfigureAwait(false);

        await cts.CancelAsync();

        foreach (Task<T> task in tasks)
        {
            await task.ContinueWith(t => { Console.WriteLine($"Task {t.Id} is faulted"); },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        return await completed;
    }

    private static void Log(Task<object> task)
    {
    }

    public static Task<T[]> WhenAllOrFirstException<T>(IEnumerable<Task<T>> tasks)
    {
        var inputs = tasks.ToList();
        var ce = new CountdownEvent(inputs.Count);
        var tcs = new TaskCompletionSource<T[]>();

        Action<Task> onCompleted = completed =>
        {
            if (completed.IsFaulted)
            {
                tcs.TrySetException(completed.Exception.InnerExceptions);
            }

            if (ce.Signal() && !tcs.Task.IsCompleted)
            {
                tcs.TrySetResult(inputs.Select(t => t.Result).ToArray());
            }
        };

        foreach (var t in inputs) t.ContinueWith(onCompleted);
        return tcs.Task;
    }
}

public class AsyncCache<TKey, TValue>(Func<TKey, Task<TValue>> valueFactory)
{
    private readonly ConcurrentDictionary<TKey, Lazy<Task<TValue>>> _map = new();

    private readonly Func<TKey, Task<TValue>> _valueFactory =
        valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));

    public Task<TValue> this[TKey key]
    {
        get
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return _map.GetOrAdd(key, toAdd =>
                new Lazy<Task<TValue>>(() => _valueFactory(toAdd))).Value;
        }
    }
}