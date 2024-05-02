using DesignPatternC_.Builder;

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person) : base(person)
    { }
    public PersonAddressBuilder At(string streetAddress)
    {
        PersonBeingBuilt.StreetAddress = streetAddress;
        return this;
    }
    public PersonAddressBuilder WithPostcode(string postcode)
    {
        PersonBeingBuilt.Postcode = postcode;
        return this;
    }
    public PersonAddressBuilder In(string city)
    {
        PersonBeingBuilt.City = city;
        return this;
    }
};