using HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

namespace HeadFirstDesignPatterns.Strategy;

public class MallardDuck: Duck
{
    public MallardDuck( ) : base(new FlyWithWings(), new LoudlyQuack())
    {
    }
}