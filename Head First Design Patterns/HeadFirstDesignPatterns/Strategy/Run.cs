namespace HeadFirstDesignPatterns.Strategy;

public class Runner
{
    public static void Run()
    {
        var mallardDuck = new MallardDuck();
        mallardDuck.PerformFly();
        mallardDuck.PerformQuack();

        Console.WriteLine("Rubber Duck in Action");
        var rubberDuck = new RubberDuck();
        rubberDuck.PerformFly();
        rubberDuck.PerformQuack();
    }
}