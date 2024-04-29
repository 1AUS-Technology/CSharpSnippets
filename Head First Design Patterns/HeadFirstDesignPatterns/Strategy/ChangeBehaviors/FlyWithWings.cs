namespace HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

public class FlyWithWings: IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("I'm flying with wings");
    }
}