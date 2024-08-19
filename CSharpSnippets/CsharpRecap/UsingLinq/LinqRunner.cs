namespace CsharpRecap.UsingLinq;

public class LinqRunner
{
    public static void Run()
    {
        //The resulting sequence from a zip operation is never longer in length than the shortest sequence
        // An int array with 7 elements.
        IEnumerable<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        // A char array with 6 elements.
        IEnumerable<char> letters = ['A', 'B', 'C', 'D', 'E', 'F'];

        // A string array with 8 elements.
        IEnumerable<string> emoji = ["🤓", "🔥", "🎉", "👀", "⭐", "💜", "✔", "💯"];

        foreach (var (number, letter) in numbers.Zip(letters))
        {
            Console.WriteLine($"{number} zipped with {letter}");   
        }

        foreach ((int number, char letter, string em) in numbers.Zip(letters, emoji))
        {
            Console.WriteLine(
                $"Number: {number} is zipped with letter: '{letter}' and emoji: {em}");
        }
    }
}