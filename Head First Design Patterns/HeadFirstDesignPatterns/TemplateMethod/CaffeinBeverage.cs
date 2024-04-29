namespace HeadFirstDesignPatterns.TemplateMethod;

public abstract class CaffeineBeverage
{
    public virtual void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        AddCondiments();
    }

    private void PourInCup()
    {
        Console.WriteLine("Pouring in cup");
    }

    private void BoilWater()
    {
        Console.WriteLine("Boiling water");
    }

    public abstract void Brew();
    
    public abstract void AddCondiments();
}