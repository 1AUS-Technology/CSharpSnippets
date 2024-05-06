namespace DesignPatternC_.Prototypes;

public interface ISupportDeepCopy<T> where T: new()
{
    void CopyTo(T target);
    public T DeepCopy()
    {
        T t = new T();
        CopyTo(t);
        return t;
    }
}


