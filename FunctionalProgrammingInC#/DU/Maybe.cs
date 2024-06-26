﻿namespace FunctionalProgrammingInC_.DU;

public abstract class Maybe<T>
{
}

public class Something<T> : Maybe<T>
{
    public Something(T value)
    {
        this.Value = value;
    }

    public T Value { get; init; }
}

public class Nothing<T> : Maybe<T>
{

}

public class Error<T> : Maybe<T>
{
    public Error(Exception e)
    {
        this.CapturedError = e;
    }

    public Exception CapturedError { get; init; }
}