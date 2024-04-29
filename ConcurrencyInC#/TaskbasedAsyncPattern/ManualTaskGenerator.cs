using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


    public static Task<int> WriteStreamAsync(this Stream stream, byte[] buffer, int offset, int count)
    {
        TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();

        stream.BeginWrite(buffer, offset, count, iar =>
        {
            try
            {
                stream.EndWrite(iar);
                tcs.SetResult(count);
            }
            catch (Exception exc)
            {
                tcs.SetException(exc);
            }
        }, null);

        return tcs.Task;
    }

    public static Task<string> DownloadStringAsync(this WebClient client, Uri uri)
    {
        TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

        client.DownloadStringCompleted += (sender, e) =>
        {
            if (e.Error != null)
            {
                tcs.SetException(e.Error);
            }
            else if (e.Cancelled)
            {
                tcs.SetCanceled();
            }
            else
            {
                tcs.SetResult(e.Result);
            }
        };

        client.DownloadStringAsync(uri);
        return tcs.Task;
    }

    public static Task<DateTimeOffset> Delay(TimeSpan delayTime)
    {
        TaskCompletionSource<DateTimeOffset> tcs = new TaskCompletionSource<DateTimeOffset>();

        //Timer timer = null;

        Timer timer = null;
        timer = new Timer(delegate
        {
            //remember to dispose the timer
            timer?.Dispose();

            //set the result
            tcs.SetResult(DateTimeOffset.UtcNow);
        }, null, Timeout.Infinite, Timeout.Infinite);

        // Change the timer to fire after the delay time
        timer.Change(delayTime, Timeout.InfiniteTimeSpan);

        return tcs.Task;
    }

    public static Task ProcessAsTaskCompletes()
    {
        //List<Task<Bitmap>> imageTasks =
        //    (from imageUrl in urls select GetBitmapAsync(imageUrl)).ToList();
        //while (imageTasks.Count > 0)
        //{
        //    try
        //    {
        //        Task<Bitmap> imageTask = await Task.WhenAny(imageTasks);
        //        imageTasks.Remove(imageTask);

        //        Bitmap image = await imageTask;
        //        panel.AddImage(image);
        //    }
        //    catch { }
        //}

        return Task.CompletedTask;
    }



}