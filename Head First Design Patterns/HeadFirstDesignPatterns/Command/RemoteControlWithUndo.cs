namespace HeadFirstDesignPatterns.Command;

public class RemoteControlWithUndo
{
    public RemoteControlWithUndo()
    {
        OnCommands = new ICommand[7];
        OffCommands = new ICommand[7];
        UndoCommand = new NoCommand();
        ICommand noCommand = new NoCommand();
        for (int i = 0; i < 7; i++)
        {
            OnCommands[i] = noCommand;
            OffCommands[i] = noCommand;
        }
    }

    public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
    {
        OnCommands[slot] = onCommand;
        OffCommands[slot] = offCommand;
    }

    public void OnButtonWasPushed(int slot)
    {
        OnCommands[slot].Execute();
        UndoCommand = OnCommands[slot];
    }

    public void UndoButtonWasPushed()
    {
        UndoCommand.Undo();
    }

    public ICommand UndoCommand { get; set; }

    public ICommand[] OnCommands { get; }
    public ICommand[] OffCommands { get; }
}

public class NoCommand : ICommand
{
    public void Execute()
    {
    }

    public void Undo()
    {
    }
}