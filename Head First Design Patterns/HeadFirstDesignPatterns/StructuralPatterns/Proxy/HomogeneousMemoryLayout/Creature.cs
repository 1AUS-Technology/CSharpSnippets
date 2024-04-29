namespace HeadFirstDesignPatterns.StructuralPatterns.Proxy.HomogeneousMemoryLayout;

public struct Creature
{
    private readonly Creatures _creatures;
    private readonly int _index;

    public Creature(Creatures creatures, int index)
    {
        _creatures = creatures;
        _index = index;
    }
    public ref byte Age => ref _creatures.CreatetureAges[_index];
    public ref int XCordinate => ref _creatures.XCordinates[_index];
    public ref int YCordinate => ref _creatures.YCordinates[_index];
}