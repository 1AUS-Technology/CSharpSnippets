using System.Collections;

namespace CsharpRecap.Iterators;

public class DaysOfTheWeek : IEnumerable
{
    private string[] days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < days.Length; i++)
        {
            yield return days[i];
        }
    }
}