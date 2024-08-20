namespace CsharpRecap.UsingLinq;

public static class GroupByContiguousKeys
{
    // The source sequence.
    private static readonly KeyValuePair<string, string>[] list =
    [
        new KeyValuePair<string, string>("A", "We"),
        new KeyValuePair<string, string>("A", "think"),
        new KeyValuePair<string, string>("A", "that"),
        new KeyValuePair<string, string>("B", "LINQ"),
        new KeyValuePair<string, string>("C", "is"),
        new KeyValuePair<string, string>("A", "really"),
        new KeyValuePair<string, string>("B", "cool"),
        new KeyValuePair<string, string>("B", "!")
    ];

    // Query variable declared as class member to be available
    // on different threads.
    private static readonly IEnumerable<IGrouping<string, KeyValuePair<string, string>>> query =
        list.ChunkBy(p => p.Key);

    public static void GroupByContiguousKeys1()
    {
        // ChunkBy returns IGrouping objects, therefore a nested
        // foreach loop is required to access the elements in each "chunk".
        foreach (var item in query)
        {
            Console.WriteLine($"Group key = {item.Key}");
            foreach (var inner in item)
            {
                Console.WriteLine($"\t{inner.Value}");
            }
        }
    }
}