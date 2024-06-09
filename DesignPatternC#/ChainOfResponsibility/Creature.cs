namespace DesignPatternC_.ChainOfResponsibility;

public class Creature
{
    public string Name;
    public int Attack, Defense;

    public Creature(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }

    public override string ToString()
    {
        return $"{Name} has {Attack} attacks and {Defense} defenses";
    }
}