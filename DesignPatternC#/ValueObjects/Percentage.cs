namespace DesignPatternC_.ValueObjects;

public record struct Percentage(decimal value)
{
    public decimal value { get; set; } = value;

    public static decimal operator *(decimal f, Percentage p)
    {
        return f * p.value;
    }

    public static Percentage operator +(Percentage a, Percentage b)
    {
        return new Percentage(a.value + b.value);
    }

    public override string ToString()
    {
        return $"{value * 100}%";
    }
}

public static class PercentageExtensions
{
    public static Percentage Percent(this int value)
    {
        return new Percentage(value / 100.0m);
    }
    public static Percentage Percent(this decimal value)
    {
        return new Percentage(value / 100.0m);
    }
}