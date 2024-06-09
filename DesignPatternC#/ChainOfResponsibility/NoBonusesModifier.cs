namespace DesignPatternC_.ChainOfResponsibility;

public class NoBonusesModifier : CreatureModifier
{
    public NoBonusesModifier(Creature creature)
        : base(creature) { }
    public override void Handle()
    {
        Console.WriteLine("No bonuses for you!");
        // no call to base.Handle() here
    }
}