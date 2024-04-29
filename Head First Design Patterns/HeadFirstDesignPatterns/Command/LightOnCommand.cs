namespace HeadFirstDesignPatterns.Command;

public class LightOnCommand: ICommand
{
    private readonly CeilingLight _light;

    public LightOnCommand(CeilingLight light)
    {
        _light = light;
    }
    public void Execute()
    {
        _light.On();
    }

    public void Undo()
    {
        _light.Off();
    }
}