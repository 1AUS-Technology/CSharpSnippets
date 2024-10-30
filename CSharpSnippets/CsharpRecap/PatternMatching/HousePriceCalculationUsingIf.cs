namespace CsharpRecap.PatternMatching;

public class HousePriceCalculationUsingIf
{
    public decimal CalculateHousePrice(House house)
    {
        if (house.LotSize >= 500)
        {
            if (house.NumberOfBedRooms > 4)
            {
                return 1.5m;
            }

            if (house.NumberOfBedRooms == 4)
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 1.1m;
                }
                else
                {
                    return 1m;
                }
            }

            if (house.NumberOfBedRooms == 3)
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 0.9m;
                }
                else
                {
                    return 0.8m;
                }
            }
            else
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 0.7m;
                }
                else
                {
                    return 0.6m;
                }
            }
        }
        else
        {
            if (house.NumberOfBedRooms > 4)
            {
                return 1.3m;
            }

            if (house.NumberOfBedRooms == 4)
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 1.0m;
                }
                else
                {
                    return 0.9m;
                }
            }

            if (house.NumberOfBedRooms == 3)
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 0.8m;
                }
                else
                {
                    return 0.7m;
                }
            }
            else
            {
                if (house.NumberOfBathRooms >= 2)
                {
                    return 0.6m;
                }
                else
                {
                    return 0.5m;
                }
            }
        }
    }
}

public class House
{
    public required uint LotSize { get; set; }
    public required uint NumberOfBedRooms { get; set; }

    public required uint NumberOfBathRooms { get; set; }
}