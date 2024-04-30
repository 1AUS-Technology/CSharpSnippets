using System.Runtime.InteropServices.JavaScript;

namespace ConcurrencyInC_.Threading;

public static class WrapSyncInAsyncExtensions
{
    //Calling this method is equivalent to calling Task.Factory.StartNew
    //with the TaskCreationOptions.LongRunning option to request a non-pooled thread.
    public static Task<T> RunAsync<T>(this Func<T> func)
    {
        var tcs = new TaskCompletionSource<T>();
        new Thread(() =>
        {
            try
            {
                var result = func();
                tcs.TrySetResult(result);
            }
            catch (Exception exception)
            {
                tcs.SetException(exception);
            }
        }).Start();

        return tcs.Task;
    }
}