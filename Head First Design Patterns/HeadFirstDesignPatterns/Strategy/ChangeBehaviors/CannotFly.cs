namespace HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

public class CannotFly:IFlyBehavior
{
    public void Fly()
    {
        Console.WriteLine("I cannot fly");
    }
}