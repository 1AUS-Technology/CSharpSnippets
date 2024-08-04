namespace PipelineProcessing
{
    public abstract class PipelineBase(IServiceProvider serviceProvider)
    {
        public async Task<PipelineContext> Execute(PipelineContext context)
        {
            var pipelineDefinition = ConfigurePipeline();

            await using var scope = serviceProvider.CreateAsyncScope();

            IEnumerable<IPipelineStep> steps = CreateStepsFromTypes(pipelineDefinition, scope);


            foreach (var step in steps)
            {
                context = await step.Execute(context);
                if (context.CancelExecution)
                {
                    break;
                }
            }

            return context;
        }

        private static IEnumerable<IPipelineStep> CreateStepsFromTypes(PipelineDefinition pipelineDefinition, AsyncServiceScope scope)
        {
            foreach (var stepType in pipelineDefinition.Steps)
            {
                var step = scope.ServiceProvider.GetService(stepType) as IPipelineStep;
                if (step == null)
                {
                    throw new InvalidOperationException($"Cannot find an implementation of {stepType} step");
                }

                yield return step;
            }
        }

        protected abstract PipelineDefinition ConfigurePipeline();
    }
}