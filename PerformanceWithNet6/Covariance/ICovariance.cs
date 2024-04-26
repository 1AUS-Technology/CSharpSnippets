namespace PerformanceWithNet6.Covariance;

public interface ICovariance<out T>
{
}

public class Covariance<T> : ICovariance<T>
{
}

public class Person
{
}

public class Employee : Person
{
}

public class Manager : Employee
{
}

public class CovarianceExample
{
    public static void Run()
    {
        ICovariance<Person> person = new Covariance<Person>();
        ICovariance<Employee> employee = new Covariance<Employee>();
        ICovariance<Manager> manager = new Covariance<Manager>();

        CovariantMethod(employee);
    }

    public static void CovariantMethod(ICovariance<Person> person)
    {
        Console.WriteLine($"The type of person passed in is of type {person.GetType()}.");
    }
}