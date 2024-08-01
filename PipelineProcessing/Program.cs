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

        IPipeline restaurantAdmissionPipeline = new RestaurantAdmissionPipeline(new ServiceContainer());
        restaurantAdmissionPipeline.Execute(young);
    }
}

public class Person
{
    public int Age { get; set; }
    public string Name { get; set; }
    public bool WellDressed { get; set; }
}

public abstract class Pipeline(IServiceProvider serviceProvider) : IPipeline
{
    private readonly IList<Type> stepTypes = new List<Type>();
    public PipelineContext Execute<T>(T argument)
    {
        //Get the steps

        // Execute steps

        return new PipelineContext();
    }

    public Pipeline AddStep<T>() where T : IPipelineStep
    {
        stepTypes.Add(typeof(T));
        return this;
    }
}

internal class RestaurantAdmissionPipeline : Pipeline
{
    public RestaurantAdmissionPipeline(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        AddStep<CheckAgeStep>().
        AddStep<CheckDressStep>().
        AddStep<CheckNameStep>();
    }
}

internal class CheckNameStep : IPipelineStep
{
    public Task Execute(PipelineContext context)
    {
        throw new NotImplementedException();
    }
}

internal class CheckDressStep : IPipelineStep
{
    public Task Execute(PipelineContext context)
    {
        throw new NotImplementedException();
    }
}

internal class CheckAgeStep : IPipelineStep
{
    public Task Execute(PipelineContext context)
    {
        throw new NotImplementedException();
    }
}

internal interface IPipeline
{
    PipelineContext Execute<T>(T argument);
}