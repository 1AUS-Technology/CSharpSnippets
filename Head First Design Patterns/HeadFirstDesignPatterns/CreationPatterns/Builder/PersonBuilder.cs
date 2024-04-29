namespace HeadFirstDesignPatterns.CreationPatterns.Builder;

public class PersonBuilder
{
    // This is the complex object we need to construct
    protected Person person;

    public PersonBuilder()
    {
        person = new Person();
    }

    protected PersonBuilder(Person person)
    {
        this.person = person;
    }

    public PersonalAddressBuilder Lives => new(person);
    public PersonJobBuilder Works => new(person);

    public static implicit operator Person(PersonBuilder pb)
    {
        return pb.person;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person) : base(person)
    {
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int annualIncome)
    {
        person.AnnualIncome = annualIncome;
        return this;
    }
}

public class PersonalAddressBuilder : PersonBuilder
{
    public PersonalAddressBuilder(Person person) : base(person)
    {
    }

    public PersonalAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonalAddressBuilder WithPostcode(string postcode)
    {
        person.Postcode = postcode;
        return this;
    }

    public PersonalAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}