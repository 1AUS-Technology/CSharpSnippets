namespace HeadFirstDesignPatterns.StatePattern;

public class NoQuarterState: IState
{
    private readonly GumballMachine _machine;

    public NoQuarterState(GumballMachine machine)
    {
        _machine = machine;
    }
    public void InsertQuarter()
    {
        Console.WriteLine("Thank you very much for your quarter");
        _machine.MoveToHasQuarterState();
    }

    public void EjectQuarter()
    {
        Console.WriteLine("You have not inserted a quarter");
    }

    public void TurnCrank()
    {
        Console.WriteLine("Turn crank but no money?");
    }

    public void Dispense()
    {
        Console.WriteLine("You need to pay first");
    }
}