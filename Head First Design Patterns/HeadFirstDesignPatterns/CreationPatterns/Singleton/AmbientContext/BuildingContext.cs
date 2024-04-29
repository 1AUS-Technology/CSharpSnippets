namespace HeadFirstDesignPatterns.CreationPatterns.Singleton.AmbientContext;

public sealed class BuildingContext : IDisposable
{
    private static readonly Stack<BuildingContext> stack = new();

    static BuildingContext()
    {
        stack.Push(new BuildingContext());
    }

    private BuildingContext()
    {
    }

    public int Height { get; private set; }

    //Get the current context
    public static BuildingContext Current => stack.Peek();
    public void Dispose()
    {
        // Remove the last context
        if (stack.Count > 1)
        {
            stack.Pop();
        }
    }

    public static IDisposable WithHeight(int height)
    {
        var context = new BuildingContext();
        context.Height = height;
        stack.Push(context);
        return context;
    }
}