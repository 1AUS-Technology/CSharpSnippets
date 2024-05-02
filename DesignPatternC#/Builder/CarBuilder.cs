namespace DesignPatternC_.Builder;

public class CarBuilder
{
    public static ISpecificCarType Create()
    {
        return new CarAssemblyLine();
    }

    private class CarAssemblyLine: ISpecificCarType, ISpecificWheelSize, IBuildCar
    {
        private readonly Car car = new Car();
        public Car Build()
        {
            return car;
        }

        public ISpecificWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this;
        }

        public IBuildCar WithWheels(int size)
        {
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheels for {car.Type}.");
            }
            car.WheelSize = size;
            return this;
        }
    }
}

public interface IBuildCar
{
    public Car Build();
}
public interface ISpecificCarType
{
    ISpecificWheelSize OfType(CarType type);
}

public interface ISpecificWheelSize
{
    public IBuildCar WithWheels(int size);
}

public enum CarType { Sedan, Crossover };
public class Car
{
    public CarType Type;
    public int WheelSize;
}