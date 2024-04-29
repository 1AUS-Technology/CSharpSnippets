using HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility.PointerLink;

namespace HeadFirstDesignPatterns.BehaviorialPatterns.ChainOfResponsibility;

public class ChainOfResponsibilityRunner
{
    public static void Run()
    {
        var goblin = new Creature("Goblin", 1, 1);
        Console.WriteLine(goblin);

        var root = new CreatureModifier(goblin);

        Console.WriteLine("Let's double goblin's attack");
        root.Add(new DoubleAttackModifier(goblin));
        root.Add(new DoubleAttackModifier(goblin));

        Console.WriteLine("Let's increase goblin's defense");
        root.Add(new IncreaseDefenseModifier(goblin));

        root.Handle();
        Console.WriteLine(goblin);
    }
}

