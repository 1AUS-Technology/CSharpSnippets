namespace HeadFirstDesignPatterns.CreationPatterns.Prototype;

public class Person : IDeepCopyable<Person>
{
    public string[] Name { get; set; }
    public Address Address { get; set; }

    public Person DeepCopy()
    {
        var person = new Person
        {
            Name = (string[])Name.Clone(),
            Address = Address.DeepCopy()
        };

        return person;
    }
}

public class Address(int streetNumber, string country, string city, string postalCode, string street)
    : IDeepCopyable<Address>
{
    public Address(int streetNumber, string streetName) : this(streetNumber, string.Empty, string.Empty, string.Empty,
        streetName)
    {
    }

    public int StreetNumber { get; set; } = streetNumber;
    public string Country { get; set; } = country;
    public string City { get; set; } = city;
    public string PostalCode { get; set; } = postalCode;
    public string Street { get; set; } = street;


    public Address DeepCopy()
    {
        return new Address(StreetNumber, Country, City, PostalCode, Street);
    }
}