namespace HeadFirstDesignPatterns.StatePattern;

public class GumballMachine
{
    public string Location { get; }
    private readonly HasQuarterState _hasQuarterState;
    private readonly NoQuarterState _noQuarterState;
    private readonly SoldOutState _soldOutState;
    private readonly SoldState _soldState;
    private IState _currentState;
    private int _numberOfGumballs;

    public GumballMachine(int numberOfGumballs, string location)
    {
        Location = location;
        _numberOfGumballs = numberOfGumballs;
        _noQuarterState = new NoQuarterState(this);
        _hasQuarterState = new HasQuarterState(this);
        _soldState = new SoldState(this);
        _soldOutState = new SoldOutState(this);
        _currentState = _numberOfGumballs > 0 ? _noQuarterState : _soldOutState;
    }

    public void MoveToHasQuarterState()
    {
        _currentState = _hasQuarterState;
    }

    public void InsertQuarter()
    {
        _currentState.InsertQuarter();
    }

    public void EjectQuarter()
    {
        _currentState.EjectQuarter();
    }

    public void TurnCrank()
    {
        _currentState.TurnCrank();
        _currentState.Dispense();
    }

    public void ReleaseBall()
    {
        Console.WriteLine("A gumball comes rolling out the slot...");
        if (_numberOfGumballs > 0)
        {
            _numberOfGumballs--;
        }

        if (_numberOfGumballs == 0)
        {
            MovetoSoldOutState();
        }
        else
        {
            MoveToNoQuarterState();
        }
    }

    public void MoveToNoQuarterState()
    {
        _currentState = _noQuarterState;
    }

    public void MoveToSoldState()
    {
        _currentState = _soldState;
    }

    public void MovetoSoldOutState()
    {
        _currentState = _soldOutState;
    }
}