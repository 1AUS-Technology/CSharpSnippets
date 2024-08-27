using System.Text.RegularExpressions;

namespace CsharpRecap.RegularExpressions;

public class RegularExpressionRunner
{
    public static void Run()
    {
        //ReplaceMatching();

        IdentifyDuplicateWords();
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