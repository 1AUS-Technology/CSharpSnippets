namespace FunctionalProgrammingInC_;

public static class ForkExtensions
{
    public static TOut Fork<TIn, T1, T2, TOut>(
        this TIn @this,
        Func<TIn, T1> f1,
        Func<TIn, T2> f2,
        Func<T1, T2, TOut> fout)
    {
        var p1 = f1(@this);
        var p2 = f2(@this);
        var result = fout(p1, p2);
        return result;
    }

    public static TEnd Fork<TStart, TMiddle, TEnd>(
        this TStart @this,
        Func<IEnumerable<TMiddle>, TEnd> joinFunction,
        params Func<TStart, TMiddle>[] individualFuncs
    )
    {
        var intermediateValues = individualFuncs.Select(x => x(@this));
        var returnValue = joinFunction(intermediateValues);
        return returnValue;
    }


   

}

public static class Extensions
{
    public static T Tap<T>(this T @this, Action<T> action)
    {
        action(@this);
        return @this;
    }


    public static ExecutionResult<TOut> MapWithTryCatch<TIn, TOut>(
        this TIn @this,
        Func<TIn, TOut> f)
    {
        try
        {
            var result = f(@this);
            return new ExecutionResult<TOut>
            {
                Result = result
            };
        }
        catch (Exception e)
        {
            return new ExecutionResult<TOut>
            {
                Error = e
            };
        }
    }


}

public class ExecutionResult<T>
{
    public T Result { get; init; }
    public Exception Error { get; init; }
}


public static class UnlessExtensionMethods
{
}

