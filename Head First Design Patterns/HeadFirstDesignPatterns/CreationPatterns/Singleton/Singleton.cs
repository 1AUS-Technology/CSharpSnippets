namespace HeadFirstDesignPatterns.CreationPatterns.Singleton;

// shared between threads
public abstract class Singleton<T> where T : Singleton<T>
{
    private static readonly Lazy<T?> _instance = new(() => Activator.CreateInstance(typeof(T), true) as T);
    public static T? Instance => _instance.Value;
}