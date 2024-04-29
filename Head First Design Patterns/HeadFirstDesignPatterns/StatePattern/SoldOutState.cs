namespace HeadFirstDesignPatterns.StatePattern;

public class SoldOutState: IState
{
    private readonly GumballMachine _machine;

    public SoldOutState (GumballMachine machine)    
    {
        _machine = machine;
        
    }
    public void InsertQuarter()
    {
        Console.WriteLine("No more gum balls to sell");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Eject nothing");
    }

    public void TurnCrank()
    {
        Console.WriteLine("No crank to turn");
    }

    public void Dispense()
    {
        Console.WriteLine("Cannot dispense when there is nothing");
    }
}