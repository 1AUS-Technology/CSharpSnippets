namespace DesignPatternC_.ChainOfResponsibility;

public class ChainRunner
{
    public static void Run()
    {
        var goblin = new Creature("Victoria Secret", 10, 10);
        Console.WriteLine(goblin);

        var chain = new CreatureModifier(goblin);
        chain.SetNextModifier(new DoubleAttackModifier(goblin));
        chain.SetNextModifier(new IncreaseDefenseModifier(goblin));

        // call handle

        chain.Handle();

        Console.WriteLine(goblin);
    }
}