namespace DesignPatternC_.OpenClosedPrinciple;

public interface IFilterCriteria<T>
{
    bool IsSatisfied(T item);
}