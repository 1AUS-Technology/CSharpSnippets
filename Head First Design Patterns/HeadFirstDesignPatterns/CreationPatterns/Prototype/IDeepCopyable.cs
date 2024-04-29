namespace HeadFirstDesignPatterns.CreationPatterns.Prototype;

public interface IDeepCopyable<out T>
{
    T DeepCopy();
}