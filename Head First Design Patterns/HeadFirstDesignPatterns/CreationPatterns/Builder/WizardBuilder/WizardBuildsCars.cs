namespace HeadFirstDesignPatterns.CreationPatterns.Builder.WizardBuilder;

public class WizardBuildsCars
{
    public static void Run()
    {
        var car = CarBuilder.Create()
            .WithType(CarType.Suv)
            .WithWheelSize(20)
            .Build();
    }
}