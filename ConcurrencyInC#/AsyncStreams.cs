namespace ConcurrencyInC_;

public class AsyncStreams
{
    public async IAsyncEnumerable<int> GenerateSequence()
    {
        await Task.Delay(1000); // some asynchronous work
        yield return 10;
        await Task.Delay(1000); // more asynchronous work
        yield return 13;
    }

    async IAsyncEnumerable<string> GetValuesAsync(HttpClient client)
    {
        int offset = 0;
        const int limit = 10;
        while (true)
        {
            // Get the current page of results and parse them.
            string result = await client.GetStringAsync($"https://example.com/api/values?offset={offset}&limit={limit}");
            string[] valuesOnThisPage = result.Split("\\N");

            // Produce the results for this page.
            foreach (string value in valuesOnThisPage)
                yield return value;

            // If this is the last page, we're done.
            if (valuesOnThisPage.Length != limit)
                break;

            // Otherwise, proceed to the next page.
            offset += limit;
        }
    }

    async Task ConsumeSequence()
    {
        await foreach (int number in GenerateSequence())
        {
            Console.WriteLine(number);
        }
    }

    public async Task ProcessValueAsync(HttpClient client)
    {
        await foreach (string value in GetValuesAsync(client))
        {
            await Task.Delay(100); // asynchronous work
            Console.WriteLine(value);
        }
    }
}