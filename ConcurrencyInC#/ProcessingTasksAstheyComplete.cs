namespace ConcurrencyInC_;

public class ProcessingTasksAsTheyComplete
{
    async Task<int> WorkerTask(int delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        return delay;
    }

    async Task AwaitAndProcessWorkerTaskResult(Task<int> task)
    {
        int result = await task;
        Console.WriteLine($"Worker task result: {result}");
    }

    public async Task ProcessTasks()
    {
        // Generate tasks
        var tasks = new List<Task<int>>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(WorkerTask(i));
        }

        IEnumerable<Task> awaitingTasks = tasks.Select(AwaitAndProcessWorkerTaskResult);

        //await all tasks
        await Task.WhenAll(awaitingTasks);
    }
}