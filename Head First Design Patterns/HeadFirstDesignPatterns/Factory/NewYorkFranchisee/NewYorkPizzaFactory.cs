using HeadFirstDesignPatterns.Factory.Franchisor;

namespace HeadFirstDesignPatterns.Factory.NewYorkFranchisee;

public class NewYorkPizzaFactory
{
    public Pizza CreatePizza(string type)
    {
        switch (type)
        {
            case "New York Supreme": return new NewYorkSupreme();
            default: throw new ArgumentException("Unknown pizza type");
        }
    }
}