namespace ConcurrencyInC_.Threading.UsingReaderWriterLockSlim;

public class UsingLockProvider
{
    public static void Run()
    {
        var lockProvider = new LockProvider();

        Task.Run(async () =>
        {
            await Task.Delay(1000);
            Console.WriteLine("Task 1 requests reader lock");
            using (await lockProvider.RequestReaderLockAsync())
            {

                Console.WriteLine("Task 1 acquires reader lock. Now do work");

                await Task.Delay(15000);

                Console.WriteLine("Task 1 finished work and will release the lock");
            }

        });

        Task.Run(async () =>
        {
            await Task.Delay(1000);
            Console.WriteLine("Task 2 requests reader lock");
            using (await lockProvider.RequestReaderLockAsync())
            {
                Console.WriteLine("Task 2 acquires reader lock. Now do work");

                await Task.Delay(20000);

                Console.WriteLine("Task 2 finished work and will release the lock");
            }

        });

        Task.Run(async () =>
        {
            await Task.Delay(1500);
            Console.WriteLine("Task 3 requests writer lock");
            using (await lockProvider.RequestWriterLockAsync())
            {
                Console.WriteLine("Task 3 acquires writer lock. Now do work");

                await Task.Delay(6000);

                Console.WriteLine("Task 3 finishes writing and will release the lock");
            }
        });

        Console.WriteLine("After the fact but executed first");
        
    }
}