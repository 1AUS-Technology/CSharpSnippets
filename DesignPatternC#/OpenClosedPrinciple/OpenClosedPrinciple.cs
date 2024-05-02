namespace DesignPatternC_.OpenClosedPrinciple;

public class OpenClosedPrinciple
{
    public enum Color { Red, Green, Blue }
    public enum Size { Small, Medium, Large, Huge }
    public record Product(string Name, Color Color, Size Size);

    public class ProductFilter
    {
        public IEnumerable<Product> FilterByColor
            (IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }
    }

    public IEnumerable<Product> FilterBySize
        (IEnumerable<Product> products, Size size)
    {
        foreach (var p in products)
            if (p.Size == size)
                yield return p;
    }
}