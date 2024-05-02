namespace DesignPatternC_.OpenClosedPrinciple;

public interface IFilter<T> where T : class
{
    IEnumerable<T> Filter(IEnumerable<T> input, IFilterCriteria<T> criteria);
}

public class ProductFilter : IFilter<OpenClosedPrinciple.Product>
{
    public IEnumerable<OpenClosedPrinciple.Product> Filter(IEnumerable<OpenClosedPrinciple.Product> input, IFilterCriteria<OpenClosedPrinciple.Product> criteria)
    {
        return input.Where(criteria.IsSatisfied);
    }
}