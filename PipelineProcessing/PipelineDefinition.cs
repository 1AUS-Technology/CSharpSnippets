namespace QR.Cii.CommonScheduleProcessor.Core.PipelineProcessing
{
    public class PipelineDefinition
    {
        private PipelineDefinition()
        {
        }

        public IList<Type> Steps { get; } = new List<Type>();

        public PipelineDefinition AddStep<T>() where T : IPipelineStep
        {
            return AddStepType<T>();
        }

        private PipelineDefinition AddStepType<T>() where T : IPipelineStep
        {
            Steps.Add(typeof(T));
            return this;
        }

        public PipelineDefinition ThenStep<T>() where T : IPipelineStep
        {
            return AddStepType<T>();
        }

        public static PipelineDefinition CreateNew()
        {
            return new PipelineDefinition();
        }
    }
}