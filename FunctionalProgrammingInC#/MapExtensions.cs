﻿namespace FunctionalProgrammingInC_;
public static class MapExtensionMethods
{
    public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> f) =>
        f(@this);

    public static T Map<T>(this T @this, params Func<T, T>[] transformations) =>
        transformations.Aggregate(@this, (agg, x) => x(agg));

    public static IEnumerable<R> Map<T, R>
        (this IEnumerable<T> ts, Func<T, R> f)
    {
        foreach (var t in ts)
            yield return f(t);
    }
}