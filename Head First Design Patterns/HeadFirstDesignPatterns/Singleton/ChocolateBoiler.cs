namespace HeadFirstDesignPatterns.Singleton;

// generate a singleton ChocolateBoiler class
public class ChocolateBoiler
{
    private static ChocolateBoiler? _uniqueInstance;
    private static readonly object Lock = new();
    private bool _boiled;
    private bool _empty;

    private ChocolateBoiler()
    {
        _empty = true;
        _boiled = false;
    }

    public static ChocolateBoiler GetInstance()
    {
        if (_uniqueInstance != null)
        {
            return _uniqueInstance;
        }

        lock (Lock)
        {
            _uniqueInstance ??= new ChocolateBoiler();
        }

        return _uniqueInstance;
    }

    public void Fill()
    {
        if (IsEmpty())
        {
            _empty = false;
            _boiled = false;
            // fill the boiler with a milk/chocolate mixture
        }
    }

    public void Drain()
    {
        if (!IsEmpty() && IsBoiled())
        {
            // drain the boiled milk and chocolate
            _empty = true;
        }
    }

    public void Boil()
    {
        if (!IsEmpty() && !IsBoiled())
        {
            // bring the contents to a boil
            _boiled = true;
        }
    }

    public bool IsEmpty()
    {
        return _empty;
    }

    public bool IsBoiled()
    {
        return _boiled;
    }
}