namespace CsharpRecap.CodingMistakes;

public class RealEstateAgent
{
    public decimal EstimateHousePrice(House house)
    {
        decimal price = 0;

        price += house.BathroomCount * 10000;
        price += house.BedroomCount * 20000;
        price += house.BedroomCount * 50000;

        house.EstimatedPrice = price;

        return price;
    }
}