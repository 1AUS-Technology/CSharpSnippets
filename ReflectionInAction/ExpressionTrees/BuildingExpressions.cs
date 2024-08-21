using System.Linq.Expressions;

namespace ReflectionInAction.ExpressionTrees;

public class BuildingExpressions
{
    public static void Run()
    {
        // Building  (x, y) => Math.Sqrt(x * x + y * y);
        var xParameter = Expression.Parameter(typeof(double), "x");
        var yParameter = Expression.Parameter(typeof(double), "y");

        var xSquared = Expression.Multiply(xParameter, xParameter);
        var ySquared = Expression.Multiply(yParameter, yParameter);
        var sum = Expression.Add(xSquared, ySquared);

        var squareRootMethod = typeof(Math).GetMethod("Sqrt", new[] { typeof(double) }) ?? throw new InvalidOperationException("Math.Sqrt not found!");

        var distance = Expression.Call(squareRootMethod, sum);

        var distanceLambda = Expression.Lambda(distance, xParameter, yParameter);

        var compiledDistance = distanceLambda.Compile();
        var result = compiledDistance.DynamicInvoke(3, 4);

        Console.WriteLine("distance:  " + result);

        // building expression

        // cuti => cuti > 10

        ParameterExpression numParam = Expression.Parameter(typeof(double), "cuti");
        ConstantExpression ten = Expression.Constant(10, typeof(int));

        BinaryExpression numGreaterThan10 = Expression.GreaterThan(numParam, ten);

        Expression<Func<int, bool>> lambda1 =
            Expression.Lambda<Func<int, bool>>(
                numGreaterThan10,
                new ParameterExpression[] { numParam });
    }
}