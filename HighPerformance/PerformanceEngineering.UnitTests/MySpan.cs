namespace PerformanceEngineering.UnitTests;

//Span<T> is defined in such a way that operations can be as efficient as on arrays: 
//Span<T> is a ref-like type as it contains a ref field, and ref fields can refer not only to the beginning of objects like arrays, but also to the middle of them:
public readonly ref struct MySpan<T>
{
    //First,Span<T> is a value type containing a ref and a length.
    //The ref T in the Span<T> is the same idea, simply encapsulated inside a struct
    private readonly ref T _pointer;
    private readonly int _length;

}

//Span<T> instances can only live on the stack, not on the heap. This means you can’t box spans (and thus can’t use Span<T> with existing reflection invoke APIs, for example, as they require boxing).
//It means you can’t have Span<T> fields in classes, or even in non-ref-like structs.
//It means you can’t use spans in places where they might implicitly become fields on classes, for instance by capturing them into lambdas or as locals in async methods or iterators