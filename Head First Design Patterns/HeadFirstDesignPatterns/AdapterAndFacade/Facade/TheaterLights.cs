namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class TheaterLights
{
    public void Dim(int degree)
    {
        Console.WriteLine("Dimming lights to "+degree);
    }

    public void On()
    {
        Console.WriteLine("Turn the lights on");
    }
}