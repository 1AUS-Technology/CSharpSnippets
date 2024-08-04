namespace PipelineProcessing
{
    public interface IPipeline
    {
        Task<PipelineContext> Execute(PipelineContext context);
    }
}