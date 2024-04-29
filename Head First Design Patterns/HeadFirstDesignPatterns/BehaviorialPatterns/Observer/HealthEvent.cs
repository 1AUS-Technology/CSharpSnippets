namespace HeadFirstDesignPatterns.BehaviorialPatterns.Observer;

public abstract class HealthEvent
{
    
}

public class BeingSickHealthEvent : HealthEvent
{
    public BeingSickHealthEvent(string sickness)
    {
        Sickness = sickness;
    }

    public string Sickness { get; }
    public DateTime When { get; } = DateTime.Now;
}