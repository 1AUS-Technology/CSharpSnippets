using HeadFirstDesignPatterns.SOLID;

namespace HeadFirstDesignPatterns.StructuralPatterns.Composite;

public abstract class CompositeFilterCriteria<T> : FilterCriteria<T>
{
    protected readonly FilterCriteria<T>[] _criteria;

    public CompositeFilterCriteria(params FilterCriteria<T>[] criteria)
    {
        _criteria = criteria;
    }
}

public class AndFilterCriteria<T> : CompositeFilterCriteria<T>
{
    public AndFilterCriteria(params FilterCriteria<T>[] criteria) : base(criteria)
    {
    }

    public override bool IsMatched(T item)
    {
        return _criteria.All(c => c.IsMatched(item));
    }
}

public class OrFilterCriteria<T> : CompositeFilterCriteria<T>
{
    public OrFilterCriteria(params FilterCriteria<T>[] criteria) : base(criteria)
    {
    }

    public override bool IsMatched(T item)
    {
        return _criteria.Any(c => c.IsMatched(item));
    }
}