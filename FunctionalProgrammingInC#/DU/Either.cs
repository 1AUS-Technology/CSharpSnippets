namespace FunctionalProgrammingInC_.DU;

public abstract class Either<T1, T2>
{
    
}

public class Left<T1, T2> : Either<T1, T2>
{
    public Left(T1 value)
    {
        this.Value = value;
    }

    public T1 Value { get; init; }
}


public class Right<T1, T2> : Either<T1, T2>
{
    public Right(T2 value)
    {
        this.Value = value;
    }

    public T2 Value { get; init; }
}