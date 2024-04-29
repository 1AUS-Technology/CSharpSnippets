namespace HeadFirstDesignPatterns.SOLID;

public enum Color
{
    Red,
    Green,
    Blue
}

public enum Size
{
    Small,
    Medium,
    Large,
    Huge
}

public record Product(string Name, Color Color, Size Size);



// you will need to change this class every time you want to add a new filter
public class ProductFilter
{
    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        return products.Where(product => product.Color == color);
    }

    public IEnumerable<Product> FilterBySize
        (IEnumerable<Product> products, Size size)
    {
        return products.Where(p => p.Size == size);
    }
}

// we introduce two new interfaces that are open for extension
// but closed for modification

public abstract class FilterCriteria<T>
{
    public abstract bool IsMatched(T item);
    public static FilterCriteria<T> operator &(FilterCriteria<T> first, FilterCriteria<T> secondCriteria)
    {
        return new AndFilter<T>(first, secondCriteria);
    }
}

public class AndFilter<T>(FilterCriteria<T> firstCriteria, FilterCriteria<T> secondCriteria) : FilterCriteria<T>
{
    public override bool IsMatched(T item)
    {
        return firstCriteria.IsMatched(item) && secondCriteria.IsMatched(item);
    }
    
}
public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, FilterCriteria<T> criteria);
}

public class ExtensibleProductFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, FilterCriteria<Product> criteria)
    {
        return items.Where(criteria.IsMatched);
    }
}
// we implement the interfaces for our product class

public class FilterByColor(Color color) : FilterCriteria<Product>
{
    public override bool IsMatched(Product item)
    {
        return item.Color == color;
    }
}

public class FilterBySize(Size size) : FilterCriteria<Product>
{
    public override bool IsMatched(Product item)
    {
        return item.Size == size;
    }
}

public class OpenClosedPrinciple
{
    public static void Run()
    {
        var apple = new Product("Apple", Color.Green, Size.Small);
        var tree = new Product("Tree", Color.Green, Size.Large);
        var house = new Product("House", Color.Blue, Size.Huge);

        var products = new List<Product> { apple, tree, house };

        var productFilter = new ProductFilter();
        
        Console.WriteLine("Green products (simple):");
        foreach (var product in productFilter.FilterByColor(products, Color.Green))
        {
            Console.WriteLine($" - {product.Name} is green");
        }

        // we can now add new filters without modifying the existing code
        var newFilter = new ExtensibleProductFilter();
        Console.WriteLine("Green products (extensible):");

        var smallAndGreen = Color.Green.MakesColorAndSizeFilter(Size.Small);
        foreach (var product in newFilter.Filter(products, smallAndGreen))
        {
            Console.WriteLine($" - {product.Name} is green");
        }
    }


   
}

public static class CriteriaExtensions
{
    public static AndFilter<Product> MakesColorAndSizeFilter(this Color color, Size size)
    {
        return new AndFilter<Product>(
            new FilterByColor(color),
            new FilterBySize(size));
    }
}