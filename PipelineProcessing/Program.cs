using System.ComponentModel.Design;

namespace PipelineProcessing;

internal class Program
{
    private static void Main(string[] args)
    {
        var young = new Person
        {
            Age = 16,
            WellDressed = true,
            Name = "N gan"
        };

      
    }
}

public class Person
{
    public int Age { get; set; }
    public string Name { get; set; }
    public bool WellDressed { get; set; }
}


