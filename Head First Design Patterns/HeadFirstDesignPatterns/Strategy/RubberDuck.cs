using HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

namespace HeadFirstDesignPatterns.Strategy;

public class RubberDuck: Duck
{
    public RubberDuck() : base(new CannotFly(), new CannotQuack())
    {
    }
}