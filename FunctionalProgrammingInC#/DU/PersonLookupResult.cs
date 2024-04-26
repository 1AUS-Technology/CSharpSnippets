namespace FunctionalProgrammingInC_.DU;
public abstract class PersonLookupResult
{
    public int Id { get; set; }
}

public class PersonFound : PersonLookupResult
{
    public Person Person { get; set; }
}

public class PersonNotFound : PersonLookupResult
{

}

public class ErrorWhileSearchingPerson : PersonLookupResult
{
    public Exception Error { get; set; }
}

public class PersonLooker
{
    public PersonLookupResult GetPerson(int id)
    {
        try
        {
            Person personFromDb = null;
            return personFromDb == null
                ? new PersonNotFound { Id = id }
                : new PersonFound
                {
                    Person = personFromDb,
                    Id = id
                };
        }
        catch (Exception e)
        {
            return new ErrorWhileSearchingPerson
            {
                Id = id,
                Error = e
            };
        }
    }
}