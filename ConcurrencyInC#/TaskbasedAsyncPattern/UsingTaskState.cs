namespace ConcurrencyInC_.TaskbasedAsyncPattern;

public class UsingTaskState
{
    public static async Task Run()
    {
        var tasks = new List<Task>();

        for (int i = 0; i < 3; i++)
        {
            var customerData = new CustomData
            {
                Name = $"Customer {i}",
                CreationTime = DateTime.Now.Ticks
            };
            var newTask = Task.Factory.StartNew(state =>
            {
                CustomData cst = state as CustomData;
                if (cst != null)
                {
                    cst.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                }
            }, customerData);
            tasks.Add(newTask);
        }

        await Task.WhenAll(tasks);

        foreach (var task in tasks)
        {
            var data = task.AsyncState as CustomData;

            if (data != null)
            {
                Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);
            }
        }
    }

    private class CustomData
    {
        public long CreationTime;
        public string Name;
        public int ThreadNum;
    }
}