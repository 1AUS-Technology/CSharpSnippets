namespace HeadFirstDesignPatterns.CreationPatterns.Singleton.AmbientContext;

public class HouseBuilder
{
    public void BuildHouse()
    {
        using (BuildingContext.WithHeight(2000))
        {
            Console.WriteLine("Building walls with height " + BuildingContext.Current.Height);

            using (BuildingContext.WithHeight(5000))
            {
                Console.WriteLine("Building walls with height " + BuildingContext.Current.Height);
            }

            Console.WriteLine("Now the height is " + BuildingContext.Current.Height);
        }

        Console.WriteLine("What happens now " + BuildingContext.Current.Height);
    }
}