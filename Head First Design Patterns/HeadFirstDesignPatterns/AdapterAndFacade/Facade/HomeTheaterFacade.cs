namespace HeadFirstDesignPatterns.AdapterAndFacade.Facade;

public class HomeTheaterFacade
{
    private readonly Amplifier _amp;
    private readonly Tuner _tuner;
    private readonly StreamingPlayer _player;
    private readonly Projector _projector;
    private readonly TheaterLights _lights;
    private readonly Screen _screen;
    private readonly PopcornPopper _popper;

    public HomeTheaterFacade(Amplifier amp, Tuner tuner, StreamingPlayer player,
        Projector projector, TheaterLights lights, Screen screen, PopcornPopper popper)
    {
    
        _amp = amp;
        _tuner = tuner;
        _player = player;
        _projector = projector;
        _lights = lights;
        _screen = screen;
        _popper = popper;
    }

    public void WatchMovie(string movie)
    {
        Console.WriteLine("Watching movie " + movie);

        _popper.On();
        _popper.Pop();
        _lights.Dim(10);
        _screen.Down();
        _projector.On();
        _projector.WideScreenMode();
        _amp.On();
        _amp.SetStreamingPlayer(_player);
        _amp.SetSurroundSound();
        _amp.SetVolume(5);
        _player.On();
        _player.Play(movie);
    }

    public void EndMovie()
    {
        Console.WriteLine("Shutting movie theater down...");

        _popper.Off();
        _lights.On();
        _screen.Up();
        _projector.Off();
        _amp.Off();
        _player.Stop();
        _player.Off();
    }
}