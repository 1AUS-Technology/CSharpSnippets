namespace HeadFirstDesignPatterns.AdapterAndFacade;

public class DuckTestDrive
{
    public static void Run()
    {
        var duck = new MallardDuck();
        var turkey = new WildTurkey();
        var turkeyAdapter = new TurkeyAdapter(turkey);

        Console.WriteLine("The Turkey says...");
        turkey.Gobble();
        turkey.Fly();

        Console.WriteLine("\nThe Duck says...");
        TestDuck(duck);

        Console.WriteLine("\nThe TurkeyAdapter says...");
        TestDuck(turkeyAdapter);
    }

    private static void TestDuck(IDuck duck)
    {
        duck.Quack();
        duck.Fly();
    }
}