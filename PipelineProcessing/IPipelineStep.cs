namespace QR.Cii.CommonScheduleProcessor.Core.PipelineProcessing
{
    public interface IPipelineStep
    {

        ValueTask<PipelineContext> Execute(PipelineContext context);
    }
}