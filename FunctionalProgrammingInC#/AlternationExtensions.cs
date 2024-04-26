namespace FunctionalProgrammingInC_;

public static class AlternationExtensions
{
    public static TOut Alternate<TIn, TOut>(
        this TIn @this,
        params Func<TIn, TOut>[] args) 
        => args.Select(x => x(@this)).First(x => x != null
    );
}

public static class ComposeExtensionMethods
{
    public static Func<TIn, NewTOut> Compose<TIn, OldTOut, NewTOut>(
        this Func<TIn, OldTOut> @this,
        Func<OldTOut, NewTOut> f) =>
        x => f(@this(x));


   
}