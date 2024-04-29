namespace HeadFirstDesignPatterns.StructuralPatterns.Proxy.HomogeneousMemoryLayout;

public class Creatures
{
    private readonly int _creatureCount;
    public byte[] CreatetureAges { get; }
    public int[] XCordinates { get; }
    public int[] YCordinates { get; }

    public Creatures(int creatureCount)
    {
        this._creatureCount = creatureCount;

        // layout the memory sequentially in a single array
        CreatetureAges = new byte[creatureCount];
        XCordinates = new int[creatureCount];
        YCordinates = new int[creatureCount];
    }
}