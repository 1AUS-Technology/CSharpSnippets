namespace HeadFirstDesignPatterns.Decorator;

public class HouseBlend : Beverage
{
    public override string Description => "House Blend Coffee";

    public override double Cost()
    {
        return 3.50;
    }
}