namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility.PointerLink;

public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Doubling {creature.Name}'s attack");
        creature.Attack *= 2;

        //Call the base class to propagate the event
        base.Handle();
    }
}