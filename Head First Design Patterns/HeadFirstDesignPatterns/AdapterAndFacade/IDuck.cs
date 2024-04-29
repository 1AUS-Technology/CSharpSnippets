namespace HeadFirstDesignPatterns.AdapterAndFacade;

public interface IDuck
{
    public void Quack();
    public void Fly();
}

public class MallardDuck: IDuck{
    public void Quack()
    {
        Console.WriteLine("Quack Quack");
    }

    public void Fly()
    {
        Console.WriteLine("I'm flying without wings");
    }
}