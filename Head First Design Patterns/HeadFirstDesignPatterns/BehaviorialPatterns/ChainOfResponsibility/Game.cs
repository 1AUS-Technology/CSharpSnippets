namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility;

public class Game
{
    public event EventHandler<Query> Queries;

    public void PerformQuery(object sender, Query q)
    {
        Queries?.Invoke(sender, q);
    }
}