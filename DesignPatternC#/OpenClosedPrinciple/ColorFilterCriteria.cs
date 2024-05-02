namespace DesignPatternC_.OpenClosedPrinciple;

public class ColorFilterCriteria: IFilterCriteria<OpenClosedPrinciple.Product>
{
    private readonly OpenClosedPrinciple.Color _color;

    public ColorFilterCriteria(OpenClosedPrinciple.Color color)
    {
        _color = color;
    }

    public bool IsSatisfied(OpenClosedPrinciple.Product item)
    {
        return item.Color == _color;
    }
}