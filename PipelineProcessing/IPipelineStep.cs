namespace PipelineProcessing
{
    public interface IPipelineStep
    {
    }

    public interface IPipelineStep<T> : IPipelineStep where T : PipelineContext
    {
        ValueTask<T> Execute(T context);
    }
}