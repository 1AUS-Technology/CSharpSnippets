namespace CleanCode;

public static class TransduceExtensions
{
    public static Func<IEnumerable<TIn>, TO2> ToTransducer<TIn, TO1, TO2>(
        this Func<IEnumerable<TIn>, IEnumerable<TO1>> @this, Func<IEnumerable<TO1>, TO2> aggregator) =>
        x => aggregator(@this(x));
}