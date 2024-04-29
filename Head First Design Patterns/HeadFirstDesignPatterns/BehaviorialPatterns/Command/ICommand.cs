namespace HeadFirstDesignPatterns.BehaviorialPatterns.Command;

public interface ICommand
{
    void Execute();
    void Undo();
}