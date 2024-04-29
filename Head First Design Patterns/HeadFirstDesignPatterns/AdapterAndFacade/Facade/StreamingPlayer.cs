namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class StreamingPlayer
{
    public void On()
    {
        Console.WriteLine("PLayer is on");
    }

    public void Play(string movie)
    {
        Console.WriteLine("Now playing movie "+ movie);
    }

    public void Stop()
    {
        Console.WriteLine("Player is stopped");
    }

    public void Off()
    {
        Console.WriteLine("Player is off");
    }
}