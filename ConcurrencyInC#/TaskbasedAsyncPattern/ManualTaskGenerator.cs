namespace ConcurrencyInC_.TaskbasedAsyncPattern;

public  static class ManualTaskGenerator
{
    public static Task<int> ReadStreamAsync(this Stream stream, byte[] buffer, int offset, int count)
    {
       TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

       stream.BeginRead(buffer, offset, count, iar =>
         {
              try
              {
                int bytesRead = stream.EndRead(iar);
                tcs.SetResult(bytesRead);
              }
              catch (Exception exc)
              {
                tcs.SetException(exc);
              }
         }, null);

       return tcs.Task;
    }
}