namespace HeadFirstDesignPatterns.Decorator.Inheritance;

public class BeverageInheritance
{
    public virtual double Cost()
    {
        return 0.0;
    }
}

//NONO
public class DarkRoastInheritance : BeverageInheritance
{
    public override double Cost()
    {
        return 0.99;
    }
}