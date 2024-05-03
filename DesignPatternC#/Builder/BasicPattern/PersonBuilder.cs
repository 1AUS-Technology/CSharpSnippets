namespace DesignPatternC_.Builder.BasicPattern;

public class PersonBuilder
{
    protected Person PersonBeingBuilt;

    public PersonBuilder() : this(new Person())
    {
    }

    protected PersonBuilder(Person person) => PersonBeingBuilt = person;

    public PersonAddressBuilder Lives()
    {
        return new PersonAddressBuilder(PersonBeingBuilt);
    }

    public PersonBuilder WorksAt(string companyName)
    {
        PersonBeingBuilt.CompanyName = companyName;
        return this;
    }

    public static implicit operator Person(PersonBuilder builder)
    {
        return builder.PersonBeingBuilt;
    }

}