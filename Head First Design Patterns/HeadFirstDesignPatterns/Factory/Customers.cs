using HeadFirstDesignPatterns.Factory.NewYorkFranchisee;

namespace HeadFirstDesignPatterns.Factory;

public class Customers
{
    public static void EatPizzas()
    {
        var newyorkStore = new NewYorkPizzaStore(new NewYorkPizzaFactory());
        var pizza = newyorkStore.OrderPizza("New York Supreme");

        Console.WriteLine($"Eating {pizza.Name}");

    }
}