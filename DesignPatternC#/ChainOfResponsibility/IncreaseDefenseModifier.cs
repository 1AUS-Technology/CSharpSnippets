namespace DesignPatternC_.ChainOfResponsibility;

public class IncreaseDefenseModifier(Creature creature) : CreatureModifier(creature)
{
    public override void Handle()
    {
        Console.WriteLine($"Increasing {_creature.Name}'s defense");
        _creature.Defense++;

        // call next
        base.Handle();
    }
}