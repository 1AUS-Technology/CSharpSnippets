using HeadFirstDesignPatterns.Factory.Franchisor;

namespace HeadFirstDesignPatterns.Factory.NewYorkFranchisee;

public class NewYorkPizzaStore : PizzaStore
{
    private readonly NewYorkPizzaFactory _factory;

    public NewYorkPizzaStore(NewYorkPizzaFactory factory)
    {
        _factory = factory;
    }
    protected override Pizza CreatePizza(string type)
    {
        return _factory.CreatePizza(type);
    }
}