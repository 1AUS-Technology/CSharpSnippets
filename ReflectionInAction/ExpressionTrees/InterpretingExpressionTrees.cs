using System.Linq.Expressions;

namespace ReflectionInAction.ExpressionTrees;

public class InterpretingExpressionTrees
{
    public static void Run()
    {
        //ExamineBinaryExpression();
        //ExamineAddExpression();

        Expression<Func<int>> sum = () => 1 + 2 + 3 + 4;

        var visitor = Visitor.CreateFromExpression(sum);
        visitor.Visit("");
    }

    private static void ExamineAddExpression()
    {
        Expression<Func<int, double, double>> additionExpression = (a, b) => a + b;

        Console.WriteLine($"This expression is of type {additionExpression.NodeType} and its name is {additionExpression.Name}");
        Console.WriteLine($"The return type is {additionExpression.ReturnType}");
        Console.WriteLine($"It has {additionExpression.Parameters.Count} parameters");
        
        foreach (var argumentExpression in additionExpression.Parameters)
        {
            Console.WriteLine($"\tParameter Type: {argumentExpression.Type.ToString()}, Name: {argumentExpression.Name}");
        }

        BinaryExpression bodyExpression = additionExpression.Body as BinaryExpression;

        Console.WriteLine($"The body is a {bodyExpression.NodeType} expression");
        Console.WriteLine($"The left side is a {bodyExpression.Left.NodeType} expression");

        var left = (UnaryExpression)bodyExpression.Left;
        Console.WriteLine($"\tParameter Type: {left.Type.ToString()}, Name: {left.NodeType}");
        Console.WriteLine($"The right side is a {bodyExpression.Right.NodeType} expression");
        var right = (ParameterExpression)bodyExpression.Right;
        Console.WriteLine($"\tParameter Type: {right.Type.ToString()}, Name: {right.Name}");
    }

    private static void ExamineBinaryExpression()
    {
        Expression<Func<int, bool>> expression = num => num < 6;

        //Decompose expression tree
        ParameterExpression param = expression.Parameters[0] as ParameterExpression;

        BinaryExpression? operation = expression.Body as BinaryExpression;

        ParameterExpression leftParamenter = operation.Left as ParameterExpression;
        ConstantExpression rightParameter = operation.Right as ConstantExpression;

        Console.WriteLine($"Decomposed expression: {param.Name} => {leftParamenter.Name} {operation.NodeType} {rightParameter.Value}");


        var constant = Expression.Constant(24, typeof(int));

        Console.WriteLine($"This is a/an {constant.NodeType} expression type");
        Console.WriteLine($"The type of the constant value is {constant.Type}");
        Console.WriteLine($"The value of the constant value is {constant.Value}");
    }
}