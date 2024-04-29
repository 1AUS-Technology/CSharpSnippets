namespace HeadFirstDesignPatterns.Decorator;

public class Mocha : CondimentDecorator
{
    public Mocha(Beverage beverage) : base(beverage)
    {
    }

    public override string Description => "Mocha, " + _beverage.Description;

    public override double Cost()
    {
        return 0.5 + _beverage.Cost();
    }
}