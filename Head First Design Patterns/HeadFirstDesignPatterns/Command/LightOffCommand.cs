namespace HeadFirstDesignPatterns.Command;

public class LightOffCommand:ICommand
{
    private readonly CeilingLight _light;

    public LightOffCommand(CeilingLight light)
    {
        _light = light;
    }
    public void Execute()
    {
        _light.Off();
    }

    public void Undo()
    {
        _light.On();
    }
}