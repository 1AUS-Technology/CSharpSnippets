namespace DesignPatternC_.Prototypes;

public class Person: ISupportDeepCopy<Person>
{
    public string[] Names;
    public Address Address;
    public void CopyTo(Person target)
    {
        target.Names = (string[])Names.Clone();

        target.Address = Address;
    }
    
}

public class Address: ISupportDeepCopy<Address>
{
    public string StreetName;
    public int HouseNumber;
    public Address(string streetName, int houseNumber) {  }

    public Address()
    { }


    public void CopyTo(Address target)
    {
        target.HouseNumber = HouseNumber;
        target.StreetName = StreetName;
    }
}

public class Employee : Person, ISupportDeepCopy<Employee>
{
    public int Salary;
    public void CopyTo(Employee target)
    {
        base.CopyTo(target); // <-- extension method call on base class
        target.Salary = Salary;
    }
}