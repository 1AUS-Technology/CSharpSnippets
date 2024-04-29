namespace HeadFirstDesignPatterns.BehaviorialPatterns.Iterator;

public class CreatureWithArrayBackedProperties
{
    private const int StrengthIndex = 0;
    private const int AgilityIndex = 0;
    private const int IntelligenceIndex = 0;
    private readonly int[] _stats = new int[3];

    public ref int Strength => ref _stats[StrengthIndex];
    public ref int Agility => ref _stats[AgilityIndex];
    public ref int Intelligence => ref _stats[IntelligenceIndex];
    public double AverageStat => _stats.Average();
    public int MaxStat => _stats.Max();
}