using HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

namespace HeadFirstDesignPatterns.Strategy;

public abstract class Duck
{
    private readonly IQuackBehavior _quackBehavior;
    private readonly IFlyBehavior _flyBehavior;

    public Duck(IFlyBehavior fly, IQuackBehavior quack)
    {
        _flyBehavior = fly;
        _quackBehavior = quack;
    }

    public void PerformQuack()
    {
        _quackBehavior.Quack();
    }

    public void PerformFly()
    {
        _flyBehavior.Fly();
    }
}