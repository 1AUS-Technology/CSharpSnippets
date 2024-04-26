namespace FunctionalProgrammingInC_.DU;
public abstract class Result<T>
{
}

public class Success<T> : Result<T>
{
    public Success(T value)
    {
        this.Value = value;
    }

    public T Value { get; init; }
}

public class Failure<T> : Result<T>
{
    public Failure(Exception e)
    {
        this.Error = e;
    }

    public Exception Error { get; init; }
}

