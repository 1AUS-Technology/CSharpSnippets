namespace HeadFirstDesignPatterns.Command;

public class SimpleRemoteControl
{
    private ICommand _lightOnCommand;

    public void SetFirstButtonCommand(ICommand command)
    {
        _lightOnCommand = command;
    }

    public void FirstButtonPressed()
    {
        _lightOnCommand.Execute();
    }
}