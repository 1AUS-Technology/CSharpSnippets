namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility.PointerLink;

public class IncreaseDefenseModifier : CreatureModifier
{
    public IncreaseDefenseModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        if (creature.Attack <= 2)
        {
            Console.WriteLine($"Increasing {creature.Name}'s defense");
            creature.Defense += 3;
        }

        base.Handle();
    }
}