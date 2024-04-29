using HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

namespace HeadFirstDesignPatterns.StructuralPatterns.Proxy;

public class CarProxy(Driver driver) : ICar
{
    private Car Car { get; } = new(CarType.Suv, 7);
    private Driver Driver { get; } = driver;

    public void Drive()
    {
        if (Driver.Age >= 18)
        {
            Car.Drive();
        }
        else
        {
            Console.WriteLine("Driver too young");
        }
    }
}