using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Threading.Channels;

namespace ConcurrencyInC_;

public class UseCollections
{
    public static async Task Runner()
    {
        //UseStack();
        //UseImmutableDictionary();
        await UseChannels();
    }

    private static void UseStack()
    {
        ImmutableStack<int> stack = ImmutableStack<int>.Empty;
        stack = stack.Push(1);
        //Create a new stack with the new value
        ImmutableStack<int> biggerStack = stack.Push(2);

        foreach (var i in biggerStack)
        {
            Console.WriteLine("Bigger stack value: " + i);
        }

        foreach (var i in stack)
        {
            Console.WriteLine("Stack value: " + i);
        }
    }

    private static void UseImmutableDictionary()
    {
        var sortedDictionary = ImmutableSortedDictionary<int, String>.Empty;
        sortedDictionary = sortedDictionary.Add(1, "One");
        sortedDictionary = sortedDictionary.Add(100, "Two");
        sortedDictionary = sortedDictionary.SetItem(1, "Cuti");

        foreach (var item in sortedDictionary)
        {
            Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
        }
    }

    private static void UseConcurrentDictionary()
    {
        var dictionary = new ConcurrentDictionary<int, string>();
        dictionary.AddOrUpdate(0, 
            key => "Zero", // add the value
            (key, oldValue) => "Zero"); // update if key exists

    }

    private static async Task UseChannels()
    {
        Channel<int> queue = Channel.CreateUnbounded<int>();

        // Producer code
        ChannelWriter<int> writer = queue.Writer;
        await writer.WriteAsync(10);
        await writer.WriteAsync(20);

        
        // If we do await here, the code that adds more values will not be executed because it will wait until the consume channel finishes
        Task.Run(() => ConsumeChannel(queue));


        Console.ReadLine();
        Console.WriteLine("Adding more values the writer");
        
        await writer.WriteAsync(30);
        await writer.WriteAsync(50);

        Console.ReadLine();
        Console.WriteLine("Completing the writer");
        // Complete the writer
        writer.Complete();
    }

    private static async Task ConsumeChannel(Channel<int> channel)
    {
        ChannelReader<int> reader = channel.Reader;
        while (await reader.WaitToReadAsync())
        {
            Console.WriteLine("Channel value: " + await reader.ReadAsync());
        }

        Console.WriteLine("Channel is completed");
    }

    private static async Task UseChannelWithBackPressure()
    {

        // Create the queue and drop the oldest items
        Channel<int> queue = Channel.CreateBounded<int>(
            new BoundedChannelOptions(1)
            {
                FullMode = BoundedChannelFullMode.DropOldest,
            });

        ChannelWriter<int> writer = queue.Writer;

        // This Write completes immediately.
        await writer.WriteAsync(7);

        // This Write (asynchronously) waits for the 7 to be removed
        // before it enqueues the 13.
        await writer.WriteAsync(13);

        writer.Complete();
    }
}