namespace HeadFirstDesignPatterns.Observer;

public interface ISubject
{
    void RegisterObserver(ILoloticaObserver observer);
    void RemoveObserver(ILoloticaObserver observer);
    void NotifyObservers();
}