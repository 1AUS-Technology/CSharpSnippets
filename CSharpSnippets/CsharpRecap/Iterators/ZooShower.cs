namespace CsharpRecap.Iterators;

public class ZooShower
{
    public static void Show()
    {
        Zoo theZoo = new Zoo();

        theZoo.AddMammal("Whale");
        theZoo.AddMammal("Rhinoceros");
        theZoo.AddBird("Penguin");
        theZoo.AddBird("Warbler");

        foreach (string name in theZoo)
        {
            Console.Write(name + " ");
        }
        Console.WriteLine();
        // Output: Whale Rhinoceros Penguin Warbler

        foreach (string name in theZoo.Birds)
        {
            Console.Write(name + " ");
        }
        Console.WriteLine();
        // Output: Penguin Warbler

        foreach (string name in theZoo.Mammals)
        {
            Console.Write(name + " ");
        }
        Console.WriteLine();
        // Output: Whale Rhinoceros

    }
}