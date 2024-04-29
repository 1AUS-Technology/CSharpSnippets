using System.Collections;

namespace HeadFirstDesignPatterns.StructuralPatterns.Composite;

public abstract class Scalar<T> : IEnumerable<T> where T : Scalar<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        yield return (T)this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


public interface ILoopable<out T> : IEnumerable<T>
{
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        yield return (T)this;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}