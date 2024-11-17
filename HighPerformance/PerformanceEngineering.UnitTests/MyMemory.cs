namespace PerformanceEngineering.UnitTests;

//if Span<T> can’t be stored to the heap and thus can’t be persisted across asynchronous operations, what’s the answer? Memory<T>.
//You can create a Memory<T> from an array and slice it just as you would a span, but it’s a (non-ref-like) struct and can live on the heap.
public class MyMemory<T>
{
    private readonly object _obj;
    private readonly int _index;
    private readonly int _length;

}