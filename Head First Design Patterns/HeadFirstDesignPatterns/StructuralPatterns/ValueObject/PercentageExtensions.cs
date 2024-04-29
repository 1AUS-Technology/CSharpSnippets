namespace HeadFirstDesignPatterns.StructuralPatterns.ValueObject;

public static class PercentageExtensions
{
    public static Percentage Percent(this decimal value) => new Percentage(value/100m);
    public static Percentage Percent(this int value) => new Percentage(value/100m);
}