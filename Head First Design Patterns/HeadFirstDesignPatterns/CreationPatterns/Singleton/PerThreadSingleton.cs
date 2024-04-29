namespace HeadFirstDesignPatterns.CreationPatterns.Singleton;

// Use Thread Local to create an object per thread
public sealed class PerThreadSingleton
{
    private static readonly ThreadLocal<PerThreadSingleton> _instance = new(() => new PerThreadSingleton());

    public int Id { get; private set; }

    private PerThreadSingleton()
    {
        Id = Thread.CurrentThread.ManagedThreadId;
    }

    public static PerThreadSingleton Instance => _instance.Value;
}