namespace HeadFirstDesignPatterns.Decorator;

public class Milk : CondimentDecorator
{
    public Milk(Beverage beverage) : base(beverage)
    {
    }

    public override string Description => "Milk, " + _beverage.Description;

    public override double Cost()
    {
        return 0.2 + _beverage.Cost();
    }
}