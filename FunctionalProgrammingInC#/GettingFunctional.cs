namespace FunctionalProgrammingInC_;

public class GettingFunctional
{
    public static bool IsPasswordValid(string password)
    {
        return new Func<string, bool>[]
        {
            x => x.Length >= 8,
            x => x.Any(char.IsUpper),
            x => x.Any(char.IsLower),
            x => x.Any(char.IsDigit),
        }.All(x => x(password));
    }
        
}