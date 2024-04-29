namespace HeadFirstDesignPatterns.IteratorAndCompositePatterns.Composite;

public class Menu : MenuComponent
{
    private readonly List<MenuComponent> _menuComponents = new();

    public Menu(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Description { get; set; }

    public string Name { get; set; }

    public override void Add(MenuComponent menuComponent)
    {
        _menuComponents.Add(menuComponent);
    }

    public override void Remove(MenuComponent menuComponent)
    {
        _menuComponents.Remove(menuComponent);
    }

    public override MenuComponent GetChild(int i)
    {
        return _menuComponents[i];
    }

    public override void Print()
    {
        Console.WriteLine($"\n{Name}");
        Console.WriteLine($", {Description}");
        Console.WriteLine("---------------------");

        foreach (var menuComponent in _menuComponents)
        {
            menuComponent.Print();
        }
    }
}