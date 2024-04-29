namespace HeadFirstDesignPatterns.IteratorAndCompositePatterns.Composite;

public class MenuItem : MenuComponent
{
    public MenuItem(string name, string description, bool isVegetarian, double price)
    {
        Name = name;
        Description = description;
        IsVegetarian = isVegetarian;
        Price = price;
    }

    public double Price { get; set; }

    public string Description { get; set; }

    public string Name { get; set; }

    public override void Print()
    {
        Console.Write($"  {Name}");
        if (IsVegetarian)
        {
            Console.Write("(v)");
        }

        Console.WriteLine($", {Price}");
        Console.WriteLine($"    -- {Description}");
    }
}