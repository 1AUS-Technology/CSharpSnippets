namespace HeadFirstDesignPatterns.Observer;

public class CurrentConditionsDisplay: ILoloticaObserver, IDisplay
{
    private readonly ISubject _subject;
    private float temperature;
    private float humidity;

    public CurrentConditionsDisplay(ISubject subject)
    {
        _subject = subject;
        subject.RegisterObserver(this);
    }
    public void Update(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.Display();
    }

    public void Display()
    {
        Console.WriteLine($"Current Conditions: Temperature {temperature} and Humidity {humidity}");
    }
}