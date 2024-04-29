namespace HeadFirstDesignPatterns.BehaviorialPatterns.Observer;

public class Person : IObservable<HealthEvent>
{
    private readonly HashSet<Subscription> subscriptions = new();

    public IDisposable Subscribe(IObserver<HealthEvent> observer)
    {
        var subscription = new Subscription(this, observer);
        subscriptions.Add(subscription);
        return subscription;
    }

    public void CallInSick(string reason)
    {
        var sicky = new BeingSickHealthEvent(reason);
        foreach (var subscription in subscriptions)
        {
            subscription.Notify(sicky);
        }
    }

    private class Subscription : IDisposable
    {
        internal Subscription(Person person, IObserver<HealthEvent> observer)
        {
            Person = person;
            Observer = observer;
        }

        private Person Person { get; }
        private IObserver<HealthEvent> Observer { get; }

        public void Dispose()
        {
        }

        public void Notify(BeingSickHealthEvent sicky)
        {
            Observer.OnNext(sicky);
        }
    }
}