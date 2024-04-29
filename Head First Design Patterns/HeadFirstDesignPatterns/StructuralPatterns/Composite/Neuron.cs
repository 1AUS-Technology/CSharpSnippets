using System.Collections;

namespace HeadFirstDesignPatterns.StructuralPatterns.Composite;

public class Neuron: ILoopable<Neuron>
{
    public readonly List<Neuron> In = new(), Out=new();
    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}