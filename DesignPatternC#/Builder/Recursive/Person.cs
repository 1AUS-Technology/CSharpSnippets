namespace DesignPatternC_.Builder.Recursive;

public class Person
{
    public string Name;
    public string Position;
    public DateTime DateOfBirth { get; set; }

    public class Builder : PersonBirthDateBuilder<Builder>
    {
        internal Builder() { }
    }
    public static Builder New => new Builder();
}

public abstract class PersonBuilder
{
    protected Person person = new Person();
    public Person Build()
    {
        return person;
    }
}


public class PersonInfoBuilder<T> : PersonBuilder where T : PersonInfoBuilder<T>
{
    public T Called(string name)
    {
        person.Name = name;
        return (T)this;
    }
}

public class PersonJobBuilder<T> : PersonInfoBuilder<PersonJobBuilder<T>> where T : PersonJobBuilder<T>
{
    public T WorksAsA(string position)
    {
        person.Position = position;
        return (T)this;
    }
}

public class PersonBirthDateBuilder<T>
    : PersonJobBuilder<T>
    where T : PersonBirthDateBuilder<T>
{
    public T Born(DateTime dateOfBirth)
    {
        person.DateOfBirth = dateOfBirth;
        return (T)this;
    }
}