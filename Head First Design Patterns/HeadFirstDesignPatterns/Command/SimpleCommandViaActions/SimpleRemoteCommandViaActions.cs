namespace HeadFirstDesignPatterns.Command.SimpleCommandViaActions;

public class SimpleRemoteCommandViaActions
{
    private Action _lightOnCommand;

    public void  SetFirstButtonCommand(Action action)   {
        _lightOnCommand = action;
    }

    public void FirstButtonPressed()
    {
        Console.WriteLine("Via Action");
        _lightOnCommand();
    }
}