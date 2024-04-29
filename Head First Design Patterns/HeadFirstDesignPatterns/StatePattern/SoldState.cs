namespace HeadFirstDesignPatterns.StatePattern;

public class SoldState: IState
{
    private readonly GumballMachine _machine;

    public SoldState(GumballMachine gumballMachine)
    {
        _machine = gumballMachine;
    }
    public void InsertQuarter()
    {
        Console.WriteLine("Already had a quarter");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Cannot eject when hand crank has turn");
    }

    public void TurnCrank()
    {
        Console.WriteLine("Turning twice does not give you another gumball");
    }

    public void Dispense()
    {
        _machine.ReleaseBall();
    }
}