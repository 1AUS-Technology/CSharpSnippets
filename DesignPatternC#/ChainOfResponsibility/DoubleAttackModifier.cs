namespace DesignPatternC_.ChainOfResponsibility;

public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Doubling {_creature.Name}'s attack");
        _creature.Attack *= 2;

        CallNextHandlerInTheChain();
    }

    private void CallNextHandlerInTheChain()
    {
        base.Handle();
    }
}