namespace HeadFirstDesignPatterns.CreationPatterns.Prototype;

public class PrototypeRunner
{
    public static void Run()
    {
        var trainRecord = new TrainRecord("D002", 6);

        var anotherTrain = trainRecord with { TrainNumber = "D003" };

        
        Console.WriteLine("Original Train");
        Console.WriteLine(trainRecord);
        Console.WriteLine(trainRecord.GetHashCode());

        Console.WriteLine("A copy");
        Console.WriteLine(anotherTrain);
        Console.WriteLine(anotherTrain.GetHashCode());


        // Dictionary use ToDictionary to do a deep copy
        // For a list, use Linq to convert each object using deep copy and then use ToList(), ToArray()
        var people = new Dictionary<string, Address>
        {
            ["John"] = new(38, "London Road"),
            ["Jane"] = new(72, "Jane Street")
        };
        var peopleCopies = people.ToDictionary(x => x.Key,
            x => x.Value.DeepCopy());


    }
}