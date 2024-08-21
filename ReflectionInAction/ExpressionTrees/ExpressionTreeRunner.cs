using System.Linq.Expressions;

namespace ReflectionInAction.ExpressionTrees;

public class ExpressionTreeRunner
{
    public static void Run()
    {
        Expression<Func<int,int>> addFive = num => num + 5;

        if (addFive is LambdaExpression lambdaExpression)
        {
            var parameter = lambdaExpression.Parameters[0];
            Console.WriteLine(parameter.Name);
            Console.WriteLine(parameter.Type);
        }

        var one = Expression.Constant(1, typeof(int));
        var two = Expression.Constant(2, typeof(int));

        var addition = Expression.Add(one, two);

        var location = CurrentLocationDetails.CreateNew()
            .Set(location => location.DestinationParentLocation, "Central")
            .Set(location => location.OriginParentLocation, "Mango Hill East")
            .Set(location => location.DestinationChildLocation, "Platform 4")
            .Set(location => location.OriginChildLocation, "Platform 1");

        Console.WriteLine(location.OriginParentLocation);


    }
}