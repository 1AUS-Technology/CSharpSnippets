namespace HeadFirstDesignPatterns.Command;

public class PartyModeCommand: ICommand
{
    private readonly ICommand[] _commands;

    public PartyModeCommand(ICommand[] commands)
    {
        _commands = commands;
    }
    //Turn on light, fan, and stereo
    public void Execute()
    {
        for (int i = 0; i < _commands.Length; i++)
        {
            _commands[i].Execute();
        }    
    }

    public void Undo()
    {
        for (int i = 0; i < _commands.Length; i++)
        {
            _commands[i].Undo();
        }
    }
}