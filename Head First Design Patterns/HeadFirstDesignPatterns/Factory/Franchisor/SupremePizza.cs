namespace HeadFirstDesignPatterns.Factory.Franchisor;

public class SupremePizza: Pizza
{
    public override string Name => "Supreme Pizza";

    public override void Prepare()
    {
        Console.WriteLine("Preparing supreme pizza...");
    }

    public override void Bake()
    {
        Console.WriteLine("Baking supreme pizza...");
    }

    public override void Cut()
    {
        Console.WriteLine("Cutting supreme pizza...");
    }

    public override void Box()
    {
        Console.WriteLine("Boxing supreme pizza...");
    }
}