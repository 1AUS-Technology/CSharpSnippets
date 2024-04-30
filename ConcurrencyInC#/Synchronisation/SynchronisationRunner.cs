namespace ConcurrencyInC_.Synchronisation;

public class SynchronisationRunner
{
    public static async Task Run()
    {
        var logger = new LoggingUsingTheSameId();

        var task1 =Task.Run(() => logger.LengthyOperation());
        var task2 = Task.Run(() => logger.LengthyOperation());

        await Task.WhenAll(task1, task2);
        Console.WriteLine("All Tasks finish. Now printing the stack");
        await logger.PrintStack();
    }
}