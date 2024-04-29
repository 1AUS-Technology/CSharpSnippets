namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class HomeTheaterTestDrive
{
    public static void Run()
    {
        var amp = new Amplifier();
        var tuner = new Tuner();
        var dvd = new StreamingPlayer();
        var projector = new Projector();
        var lights = new TheaterLights();
        var screen = new Screen();
        var popper = new PopcornPopper();

        var homeTheater = new HomeTheaterFacade(amp, tuner, dvd, projector, lights, screen, popper);
        homeTheater.WatchMovie("Raiders of the Lost Ark");

        Console.WriteLine("Press Enter to end watching");
        Console.ReadLine();
        homeTheater.EndMovie();
    }
}