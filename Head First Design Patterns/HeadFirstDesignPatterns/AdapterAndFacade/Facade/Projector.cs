namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class Projector
{
    public void On()
    {
        Console.WriteLine("Projector is on");
    }

    public void WideScreenMode()
    {
        Console.WriteLine("Wide mode is on");
    }

    public void Off()
    {
        Console.WriteLine("Projector is off");
    }
}