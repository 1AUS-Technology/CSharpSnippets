namespace HeadFirstDesignPatterns.TemplateMethod;

public class CoffeePreparation : CaffeineBeverage
{
    public override void Brew()
    {
        Console.WriteLine("Brewing coffee grinds");
    }

    public override void AddCondiments()
    {
        Console.WriteLine("Add sugar and milk");
    }
}