namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility.PointerLink;

public record Creature
{
    public int Attack, Defense;

    public Creature(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }

    public string Name { get; set; }
}