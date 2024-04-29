namespace HeadFirstDesignPatterns.BehaviorialPatterns.Command;

public class CompositeCommand : List<ICommand>, ICommand
{
    public void Execute()
    {
        ForEach(cmd => cmd.Execute());
    }

    public void Undo()
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            this[i].Undo();
        }
    }
}