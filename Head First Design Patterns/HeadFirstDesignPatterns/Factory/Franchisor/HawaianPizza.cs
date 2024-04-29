namespace HeadFirstDesignPatterns.Factory.Franchisor;

public class HawaianPizza:Pizza
{
    public override string Name => "Hawaian Pizza";

    public override void Prepare()
    {
        Console.WriteLine("Preparing Hawaian pizza...");
    }

    public override void Bake()
    {
        Console.WriteLine("Baking Hawaian pizza...");
    }

    public override void Cut()
    {
        Console.WriteLine("Cutting Hawaian pizza...");
    }

    public override void Box()
    {
        Console.WriteLine("Boxing Hawaian pizza...");
    }
}