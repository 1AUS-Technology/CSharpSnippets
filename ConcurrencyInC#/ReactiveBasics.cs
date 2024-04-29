using System.Reactive.Linq;

namespace ConcurrencyInC_;

public class ReactiveBasics
{
    public static void Runner()
    {
        UseBuffer();
    }

    private static void UseBuffer()
    {
        // Buffer output is IEnumerable<T> and will not pass along the value
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Buffer(3)
            .Subscribe(buffer =>
            {
                Console.WriteLine($"Buffered values: {string.Join(", ", buffer)}");
            });

        // Window output is IObservable<IObservable<T>> and will pass along the value
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Window(2)
            .Subscribe(window =>
            {
               Console.WriteLine("Starting a new window");
               window.Subscribe(
                     Console.WriteLine,
                     () => Console.WriteLine("Window completed"));

            });
    }


    private void GetWithTimeout(HttpClient client)
    {
        //client.GetStringAsync("http://www.example.com")
        //    .Timeout(TimeSpan.FromSeconds(1))
        //    .Subscribe(
        //        Console.WriteLine,
        //        ex => Console.WriteLine($"An error occurred: {ex.Message}"));
    }


}