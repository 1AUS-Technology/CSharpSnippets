namespace FunctionalProgrammingInC_.Options;

public static class OptionMatcherExtensions
{
    public static R Match<T, R>(this Option<T> opt, Func<R> None, Func<T, R> Some)
    {
        return opt switch
        {
            None<T> => None(),
            Some<T>(var t) => Some(t),
            _ => throw new ArgumentException("Option must be None or Some")
        };
    }
}