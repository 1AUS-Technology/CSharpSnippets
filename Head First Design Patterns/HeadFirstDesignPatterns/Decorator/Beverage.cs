namespace HeadFirstDesignPatterns.Decorator;

public abstract class Beverage
{
    public abstract string Description { get; }

    public abstract double Cost();
}