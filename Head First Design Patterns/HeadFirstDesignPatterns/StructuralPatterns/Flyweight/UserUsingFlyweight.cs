namespace HeadFirstDesignPatterns.StructuralPatterns.Flyweight;

public class UserUsingFlyweight
{
    // cache commonly used names
    private static List<string> cachedNames = new();

    private int[] firstAndLastNameIndexes;

    public UserUsingFlyweight(string fullName)
    {
        firstAndLastNameIndexes = fullName.Split(' ').Select(FindCachedIndex).ToArray();

        int FindCachedIndex(string s)
        {
            int index = cachedNames.IndexOf(s);
            if (index != -1)
            {
                return index;
            }

            cachedNames.Add(s);
            return cachedNames.Count - 1;
        }
    }

    public string FullName => $"{cachedNames[firstAndLastNameIndexes[0]]} {cachedNames[firstAndLastNameIndexes[1]]}";
}