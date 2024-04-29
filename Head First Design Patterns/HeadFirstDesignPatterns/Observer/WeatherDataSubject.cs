namespace HeadFirstDesignPatterns.Observer;

public class WeatherDataSubject: ISubject
{
    private ISet<ILoloticaObserver> observers;
    private float temperature;
    private float humidity;
    private float pressure;

    public WeatherDataSubject()
    {
        observers = new HashSet<ILoloticaObserver>();
    }
    public void RegisterObserver(ILoloticaObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(ILoloticaObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var loloticaObserver in observers)
        {
            loloticaObserver.Update(temperature, humidity, pressure);
        }
    }

    public void MeasurementsChanged()
    {
        NotifyObservers();
    }

    public void SetMeasurements(float temperature, float humidity, float pressure)
    {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;

        MeasurementsChanged();
    }
}