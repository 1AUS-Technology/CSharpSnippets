namespace DesignPatternC_.Builder;

public class BuilderRunner
{
    public static void Run()
    {
        var builder = new PersonBuilder();

        PersonBuilder person = builder.Lives()
            .At("100 Queen Street")
            .In("Brisbane")
            .WithPostcode("4000")
            .WorksAt("BHP");


        var car = CarBuilder.Create()
            .OfType(CarType.Crossover)
            .WithWheels(20)
            .Build();
    }
}