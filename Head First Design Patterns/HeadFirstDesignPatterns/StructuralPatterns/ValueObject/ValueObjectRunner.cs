namespace HeadFirstDesignPatterns.StructuralPatterns.ValueObject;

public class ValueObjectRunner
{
    public static void Run()
    {
        Console.WriteLine(10.Percent() + 20.Percent());
        Console.WriteLine(10 * 6.Percent());
    }
}