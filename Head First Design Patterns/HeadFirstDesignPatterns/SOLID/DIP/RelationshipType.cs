namespace HeadFirstDesignPatterns.SOLID.DIP;

public enum RelationshipType
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string Name;
}

// Low level component
public class Relationships
{
    public List<(Person, RelationshipType, Person)> relationships = new();

    public void AddParentAndChild(Person parent, Person child)
    {
        relationships.Add((parent, RelationshipType.Parent, child));
        relationships.Add((child, RelationshipType.Child, parent));
    }
}

public class Research
{
    public Research(Relationships relationships)
    {
        // Research class depends directly on the property of the Relationships class
        var relas = relationships.relationships;

        foreach (var r in relas.Where(x => x.Item1.Name == "John" && x.Item2 == RelationshipType.Parent))
        {
            Console.WriteLine($"John has a child called {r.Item3.Name}");
        }
    }

    public void ResearchBetter(IRelationshipBrowser browser)
    {
        foreach (var p in browser.FindAllChildrenOf("John"))
        {
            Console.WriteLine($"John has a child called {p.Name}");
        }
    }
}

// Split up the search to a new interface

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllChildrenOf(string name);
}

public class ImprovedRelationships : IRelationshipBrowser // low-level
{
    // no longer public!
    private readonly List<(Person, RelationshipType, Person)> relations = new();

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
        return relations.Where(x => x.Item1.Name == name && x.Item2 == RelationshipType.Parent).Select(r => r.Item3);
    }
}