namespace HeadFirstDesignPatterns.Factory.Franchisor;

public abstract class Pizza
{
    public abstract string Name { get; }
    public virtual void Prepare()
    {
        Console.WriteLine("Preparing pizza...");
    }

    public virtual void Bake()
    {
        Console.WriteLine("Baking pizza...");
    }

    public virtual void Cut()
    {
        Console.WriteLine("Cutting pizza...");
    }

    public virtual void Box()
    {
        Console.WriteLine("Boxing pizza...");
    }
}