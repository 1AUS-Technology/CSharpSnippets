namespace PipelineProcessing;

public interface IPipelineStep
{
    ValueTask<PipelineContext> Execute(PipelineContext context);
}