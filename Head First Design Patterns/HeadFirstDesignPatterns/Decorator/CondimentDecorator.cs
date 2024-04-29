namespace HeadFirstDesignPatterns.Decorator;

/// <summary>
/// This class wraps around the object that is being decorated so that it can be used in place of the object.
/// It also adds extra functionality to the object.
/// </summary>
public abstract class CondimentDecorator : Beverage
{
    protected Beverage _beverage;

    public CondimentDecorator(Beverage beverage)
    {
        _beverage = beverage;
    }
}