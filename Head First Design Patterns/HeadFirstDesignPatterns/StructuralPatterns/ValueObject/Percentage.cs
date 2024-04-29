using System.Diagnostics;

namespace HeadFirstDesignPatterns.StructuralPatterns.ValueObject;

[DebuggerDisplay("{Value*100.0f}%")]
public readonly record struct Percentage(decimal value)
{
    private readonly decimal _value = value;

    public static decimal operator *(decimal d, Percentage p) => d * p._value;
    public static Percentage operator +(Percentage p1, Percentage p2) => new (p1._value + p2._value);

    public override string ToString()
    {
        return $"{_value * 100}%";
    }
}

