namespace HeadFirstDesignPatterns.Strategy.ChangeBehaviors;

public class CannotQuack: IQuackBehavior
{
    public void Quack()
    {
        Console.WriteLine("I do not quack");
    }
}