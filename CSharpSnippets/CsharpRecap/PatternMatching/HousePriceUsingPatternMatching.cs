namespace CsharpRecap.PatternMatching;

public class HousePriceUsingPatternMatching
{
    public decimal CalculateHousePrice(House house)
    {
        return (house.LotSize, house.NumberOfBedRooms, house.NumberOfBathRooms) switch
        {
            (>= 500, > 4, _) => 1.5m,
            (>= 500, 4, >= 2) => 1.1m,
            (>= 500, 4, _) => 1m,
            (>= 500, 3, >= 2) => 0.9m,
            (>= 500, 3, _) => 0.8m,
            (>= 500, _, >= 2) => 0.7m,
            (>= 500, _, _) => 0.6m,

            (_, > 4, _) => 1.3m,
            (_, 4, >= 2) => 1.0m,
            (_, 4, _) => 0.9m,
            (_, 3, >= 2) => 0.8m,
            (_, 3, _) => 0.7m,
            (_, _, >= 2) => 0.6m,
            (_, _, _) => 0.5m,
        };
    }
}