namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class Amplifier
{
    public void On()
    {
        Console.WriteLine("Amplifier is on");

    }

    public void SetStreamingPlayer(StreamingPlayer player)
    {
        Console.WriteLine("Setting streaming player");
    }

    public void SetSurroundSound()
    {
        Console.WriteLine("Setting surround sounds");
    }

    public void SetVolume(int i)
    {
        Console.WriteLine("Volume is set to " + i);
    }

    public void Off()
    {
        Console.WriteLine("Amplifier is off");
    }
}