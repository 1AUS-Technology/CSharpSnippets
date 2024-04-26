using CleanCode;
using FunctionalProgrammingInC_.DU;

namespace FunctionalProgrammingInC_;

public class FunctionRunner
{
    public static void Run()
    {
        var numbers = UseCompose();
        UseTransduce(numbers);

        UseEither();
    }

    private static void UseEither()
    {
        var data = QuestionOrAnswer();
        var output = data switch
        {
            Left<string, int> l => "The ultimate question was: " + l.Value,
            Right<string, int> r => "The ultimate answer was: " + r.Value.ToString()
        };

        Console.WriteLine(output);
    }

    private static void UseTransduce(int[] numbers)
    {
        var transformer = (IEnumerable<int> x) => x
            .Select(y => y + 5)
            .Select(y => y * 10)
            .Where(y => y > 100);

        var aggregator = (IEnumerable<int> x) => string.Join(", ", x);

        var transducer = transformer.ToTransducer(aggregator);
        var output2 = transducer(numbers);
        Console.WriteLine("Output = " + output2);
    }

    private static int[] UseCompose()
    {
        var numbers = new[] { 4, 8, 15, 16, 23, 42 };
        var average = numbers.Fork(
            x => x.Sum(),
            x => x.Count(),
            (s, c) => s / c
        );


        string FormatDecimal(decimal x) => x.Map(x => Math.Round(x, 2)).Map(x => $"{x} degrees");

        // use Compose function
        
        var celsiusToFahrenheit = (decimal x) =>
            x.Map(x => x * 9)
                .Map(x => x / 5)
                .Map(x => x + 32);

        var composedFormatting = celsiusToFahrenheit.Compose(FormatDecimal);

        var output = composedFormatting(50);

        Console.WriteLine(output);
        return numbers;
    }

    public static Either<string, int> QuestionOrAnswer() =>
        new Random().Next(1, 6) >= 4
            ? new Left<string, int>("What do you get if you multiply 6 by 9?")
            : new Right<string, int>(42);

  

}