namespace DesignPatternC_.Builder;

public abstract class FunctionalBuilder<TObjectUnderConstruction, TBuilder>
    where TBuilder : FunctionalBuilder<TObjectUnderConstruction, TBuilder>
    where TObjectUnderConstruction : new()
{
    private readonly List<Func<TObjectUnderConstruction, TObjectUnderConstruction>> buildingFunctions = new();
    public TBuilder Do(Action<TObjectUnderConstruction> action)
        => AddAction(action);
    private TBuilder AddAction(Action<TObjectUnderConstruction> action)
    {
        buildingFunctions.Add(objectUnderConstruction =>
        {
            action(objectUnderConstruction); 
            return objectUnderConstruction;
        });
        return (TBuilder)this;
    }
    public TObjectUnderConstruction Build() => buildingFunctions.Aggregate(new TObjectUnderConstruction(), (obj, buildingFunc) => buildingFunc(obj));
}