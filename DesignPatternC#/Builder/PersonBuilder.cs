namespace DesignPatternC_.Builder;

public class PersonBuilder
{
    protected Person PersonBeingBuilt;

    public PersonBuilder() : this(new Person())
    {
    }

    protected PersonBuilder(Person person)=> this.PersonBeingBuilt = person;

    public PersonAddressBuilder Lives(string address)
    {
        return new PersonAddressBuilder(PersonBeingBuilt);
    }

    public PersonBuilder WorksAt(string companyName)
    {
        this.PersonBeingBuilt.CompanyName = companyName;
        return this;
    }

    public static implicit operator Person(PersonBuilder builder)
    {
        return builder.PersonBeingBuilt;
    }

}