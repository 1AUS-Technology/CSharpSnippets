namespace HeadFirstDesignPatterns.Decorator;

public class DarkRoast : Beverage
{
    public override string Description => "Dark Roast Coffee";

    public override double Cost()
    {
        return 2.9;
    }
}