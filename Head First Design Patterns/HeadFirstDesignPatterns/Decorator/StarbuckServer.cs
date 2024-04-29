namespace HeadFirstDesignPatterns.Decorator;

public class StarbuckServer
{
    public static void Run()
    {
        Beverage beverage = new Espresso();

        Console.WriteLine($"Expresso cost: {beverage.Cost()}");

        var addedMocha = new Mocha(beverage);

        Console.WriteLine("Added mocha cost :" + addedMocha.Cost());
    }
}