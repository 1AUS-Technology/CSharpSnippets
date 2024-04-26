namespace ConcurrencyInC_;

public class AsyncBasics
{
    public async Task<string> DownloadWithTimeout(HttpClient client, string uri, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);
        Task<string> downloadTask = client.GetStringAsync(uri);
        Task timeoutTask = Task.Delay(Timeout.InfiniteTimeSpan, cts.Token);

        Task completedTask = await Task.WhenAny(downloadTask, timeoutTask);
        if(completedTask == timeoutTask)
        {
            return null;
        }
        return await downloadTask;
    }

    public async Task ReportProgress(IProgress<double> progressReporter)
    {
        for (int i = 0; i < 100; i++)
        {
            await Task.Delay(100);
            progressReporter.Report(i);
        }
    }



}