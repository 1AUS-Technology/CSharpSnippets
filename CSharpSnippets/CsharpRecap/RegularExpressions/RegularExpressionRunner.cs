using System.Globalization;
using System.Text.RegularExpressions;

namespace CsharpRecap.RegularExpressions;

public class RegularExpressionRunner
{
    public static void Run()
    {
        //ReplaceMatching();

        //IdentifyDuplicateWords();

        CultureSensitiveNumberMatchingExpressions();
    }

    private static void CultureSensitiveNumberMatchingExpressions()
    {
        // Define text to be parsed.
        string input = "Office expenses on 2/13/2008:\n" +
                       "Paper (500 sheets)                      $3.95\n" +
                       "Pencils (box of 10)                     $1.00\n" +
                       "Pens (box of 10)                        $4.49\n" +
                       "Erasers                                 $2.19\n" +
                       "Ink jet printer                        $69.95\n\n" +
                       "Total Expenses                        $ 81.58\n";

        NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;

        string currencySymbol = numberFormatInfo.CurrencySymbol;
        bool symbolPrecedesIfPositive = numberFormatInfo.CurrencyPositivePattern % 2 == 0;
        string groupSeparator = numberFormatInfo.CurrencyGroupSeparator;
        string decimalSeparator = numberFormatInfo.CurrencyDecimalSeparator;

        string symbolBeforeNumber = Regex.Escape(symbolPrecedesIfPositive ? currencySymbol : "");
        string numberPattern = @"\s*[-+]?" + "([0-9]{0,3}(" + groupSeparator + "[0-9]{3})*(" + Regex.Escape(decimalSeparator) + "[0-9]+)?)";
        string symbolAfterNumber = (!symbolPrecedesIfPositive ? currencySymbol : "");
        string pattern =  symbolBeforeNumber + numberPattern +  symbolAfterNumber;

        Console.WriteLine("The regular expression pattern is: " + pattern);

        MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.IgnorePatternWhitespace);
        Console.WriteLine("Found {0} matches.", matches.Count);

        List<decimal> expenses = new List<decimal>();

        foreach (Match match in matches)
        {
            expenses.Add(Decimal.Parse(match.Groups[1].Value));
        }

        // Determine whether total is present and if present, whether it is correct.
        decimal total = expenses.Sum();
        Console.WriteLine("The expenses total {0:C2}.", total / 2 == expenses[^1] ? expenses[^1] : total);
    }

    private static void IdentifyDuplicateWords()
    {
        
        string pattern = @"\b(\w+?)\s\1\b";
        // \b start of a word boundary
        // (\w+?) match one or more word characters but as few characters as possible. Together, they form a group that can be referred to as \1.
        // \s Match a white-space character.
        // \1 match the substring equal to the group named \1
        // \b match a word boundary

        
        string input = "This this is a nice day. What about this? This tastes good. I saw a a dog.";
        foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
            Console.WriteLine("{0} (duplicates '{1}') at position {2}",
                match.Value, match.Groups[1].Value, match.Index);


    }

    private static void ReplaceMatching()
    {
        string pattern = "(Mr\\.? |Mrs\\.? |Miss |Ms\\.?)";

        string[] names = { "Mrs. Victoria Vu", "Mr. Thomas Nguyen", "Ms. Anna Nguyen" };

        foreach (var name in names)
        {
            Console.WriteLine(Regex.Replace(name, pattern, "*****"));
        }
    }
}