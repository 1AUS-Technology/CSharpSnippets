using HeadFirstDesignPatterns.Factory.Franchisor;

namespace HeadFirstDesignPatterns.Factory.NewYorkFranchisee;

public class NewYorkSupreme: SupremePizza
{
    public override string Name => "New York " + base.Name;

    public override void Prepare()
    {
        base.Prepare();
        Console.WriteLine("Adding New York specific ingredients...");
        Console.WriteLine("Adding peanut butter on top");
    }
}