namespace HeadFirstDesignPatterns.TemplateMethod;

public class TeaPreparation : CaffeineBeverage
{
    public override void Brew()
    {
        Console.WriteLine("Steeping tea bag");
    }

    public override void AddCondiments()
    {
        Console.WriteLine("Add Lemon");
    }
}