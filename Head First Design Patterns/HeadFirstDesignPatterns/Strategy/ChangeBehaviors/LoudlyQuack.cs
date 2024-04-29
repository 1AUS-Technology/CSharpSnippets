namespace HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

public class LoudlyQuack: IQuackBehavior
{
    public void Quack()
    {
        Console.WriteLine("I Quack loudly");
    }
}