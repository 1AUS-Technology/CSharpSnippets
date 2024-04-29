namespace HeadFirstDesignPatterns.StructuralPatterns.Proxy;

public class PropertyWithName<T> where T : new()
{
    private readonly string name;
    private T value;

    public PropertyWithName() : this(default)
    {
    }

    public PropertyWithName(T value, string name = "")
    {
        this.value = value;
        this.name = name;
    }

    public T Value
    {
        get => value;
        set
        {
            if (Equals(this.value, value)) return;
            Console.WriteLine($"Assigning {value} to {name}");
            this.value = value;
        }
    }

    public static implicit operator T(PropertyWithName<T> propertyWithName)
    {
        return propertyWithName.Value; // int n = p_int;
    }

    public static implicit operator PropertyWithName<T>(T value)
    {
        return new PropertyWithName<T>(value); // Property<int> p = 123;
    }
}