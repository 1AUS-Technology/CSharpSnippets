namespace HeadFirstDesignPatterns.StructuralPatterns.Proxy;

public class Creature
{
    private readonly PropertyWithName<int> agility = new(100, nameof(agility));

    public int Agility
    {
        get => agility.Value;
        set => agility.Value = value;
    }
}