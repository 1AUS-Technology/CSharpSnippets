namespace CleanCode;

public class GuessStatisticsMessage
{
    private string _number;
    private string _pluralModifier;
    private string _verb;

    public string Make(string candidate, int count)
    {
        CreatePluralDependentMessageParts(count);
        return $"There {_verb} {_number} {candidate}{_pluralModifier}";
    }

    private void CreatePluralDependentMessageParts(int count)
    {
        switch (count)
        {
            case 0:
                ThereAreNoLetters();
                break;
            case 1:
                ThereIsOneLetter();
                break;
            default:
                ThereAreManyLetters(count);
                break;
        }
    }

    private void ThereAreManyLetters(int count)
    {
        _number = count.ToString();
        _verb = "are";
        _pluralModifier = "s";
    }

    private void ThereIsOneLetter()
    {
        _number = "1";
        _verb = "is";
        _pluralModifier = "";
    }

    private void ThereAreNoLetters()
    {
        _number = "no";
        _verb = "are";
        _pluralModifier = "s";
    }
}