namespace QR.Cii.CommonScheduleProcessor.Core.PipelineProcessing
{
    public interface IPipeline
    {
        Task<PipelineContext> Execute(PipelineContext context);
    }
}