namespace HeadFirstDesignPatterns.Command;

// Create a command class for the ceiling fan that implements the ICommand interface.
// The constructor for the command should take a ceiling fan as a parameter.
// The command should have an Execute() method that calls the ceiling fan's High() method.
// The command should have an Undo() method that calls the ceiling fan's Off() method.

public class CeilingFanHighCommand
{
    private readonly CeilingFan _ceilingFan;
    private FanSpeed _previousSpeed;

    public CeilingFanHighCommand(CeilingFan ceilingFan)
    {
        _ceilingFan = ceilingFan;
    }

    public void Execute()
    {
        _previousSpeed = _ceilingFan.Speed;
        _ceilingFan.High();
    }

    public void Undo()
    {
        switch (_previousSpeed)
        {
            case FanSpeed.High:
                _ceilingFan.High();
                break;
            case FanSpeed.Medium:
                _ceilingFan.Medium();
                break;
            case FanSpeed.Low:
                _ceilingFan.Low();
                break;
            case FanSpeed.Off:
                _ceilingFan.Off();
                break;
        }
    }
}