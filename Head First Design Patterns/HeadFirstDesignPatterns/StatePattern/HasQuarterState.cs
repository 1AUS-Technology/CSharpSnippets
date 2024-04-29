namespace HeadFirstDesignPatterns.StatePattern;

public class HasQuarterState: IState
{
    private readonly GumballMachine _gumballMachine;

    public HasQuarterState(GumballMachine gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }
    public void InsertQuarter()
    {
        Console.WriteLine("You have already inserted a quarter.");
    }

    public void EjectQuarter()
    {
        _gumballMachine.MoveToNoQuarterState();
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned the crank.");
        _gumballMachine.MoveToSoldState();
    }

    public void Dispense()
    {
        Console.WriteLine("No gumball to dispense because you have not turned the crank");
    }
}