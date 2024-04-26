namespace FunctionalProgrammingInC_;
public class Person
{
    public string FirstName { get; set; }
    public IEnumerable<string> MiddleNames { get; set; }
    public string LastName { get; set; }
}

public class PersonRunner
{
    public static void Run()
    {
        var input = "Percy James Patrick Kent-Smith".Split(" ");

        // match middle names
        var person = new Person
        {
            FirstName = input.First(),
            MiddleNames = input is [_,..var ms, _]? ms: Enumerable.Empty<string>(),
            LastName = input.Last()
        };
    }
}