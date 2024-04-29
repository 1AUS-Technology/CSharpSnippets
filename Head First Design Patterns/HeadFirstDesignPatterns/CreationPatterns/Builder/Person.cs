namespace HeadFirstDesignPatterns.CreationPatterns.Builder;

public class Person
{
    public int AnnualIncome;

    // employment info
    public string CompanyName, Position;
    public string StreetAddress, Postcode, City;

    public override string ToString()
    {
        return
            $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
}