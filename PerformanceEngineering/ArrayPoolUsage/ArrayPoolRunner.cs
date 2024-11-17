using System.Buffers;
using System.Collections.ObjectModel;

namespace PerformanceEngineering.ArrayPoolUsage;

public class ArrayPoolRunner
{
    private static readonly ArrayPool<Car> CarPool = ArrayPool<Car>.Shared;

    public static void Run()
    {
        var carCount = 3;
        string brandName = "Toyota";

        var toyotaCars = GenerateCars(carCount, brandName);
        var mazdaCars = GenerateCars(carCount, "Mazda");

        ShowCars(toyotaCars);
        ShowCars(mazdaCars);

    }

    private static void ShowCars(IReadOnlyCollection<Car> cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Name} produced in {car.ProductionYear}. Hashcode {car.GetHashCode()}");
        }
    }

    private static IReadOnlyCollection<Car> GenerateCars(int carCount, string brandName)
    {
        var rentedCars = CarPool.Rent(carCount);
        try
        {
            for (int i = 0; i < carCount; i++)
            {
                rentedCars[i] = new Car
                                {
                                    Name = brandName + i,
                                    ProductionYear = 2020 + i
                                };
            }

            return new ReadOnlyCollection<Car>(rentedCars[..carCount]);
        }
        finally
        {
            CarPool.Return(rentedCars);
        }
    }
}