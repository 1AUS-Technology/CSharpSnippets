namespace PipelineProcessing
{
    public interface IPipeline<T> where T : PipelineContext
    {
        Task<T> Execute(T context);
    }
}