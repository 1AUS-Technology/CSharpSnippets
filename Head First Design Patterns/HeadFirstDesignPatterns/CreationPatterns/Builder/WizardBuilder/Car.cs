using HeadFirstDesignPatterns.StructuralPatterns.Proxy;

namespace HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

public class Car(CarType Type, int Seats): ICar
{
    public void Drive()
    {
        Console.WriteLine("The wheels on the bus go round and round");
    }
}