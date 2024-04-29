namespace HeadFirstDesignPatterns.Decorator;

public class Espresso : Beverage
{
    public override string Description => "Expresso Coffee";

    public override double Cost()
    {
        return 1.99;
    }
}