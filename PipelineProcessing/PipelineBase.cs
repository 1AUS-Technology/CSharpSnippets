using Microsoft.Extensions.DependencyInjection;

namespace PipelineProcessing
{
    public abstract class PipelineBase<T>(IServiceProvider serviceProvider) : IPipeline<T> where T : PipelineContext
    {
        public async Task<T> Execute(T context)
        {
            var pipelineDefinition = ConfigurePipeline();

            await using var scope = serviceProvider.CreateAsyncScope();

            IEnumerable<IPipelineStep<T>> steps = CreateStepsFromTypes(pipelineDefinition, scope);

            context = await ExecuteSteps(steps, context);

            return context;
        }

        private static async Task<T> ExecuteSteps(IEnumerable<IPipelineStep<T>> steps, T context)
        {
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

        private static IEnumerable<IPipelineStep<T>> CreateStepsFromTypes(PipelineDefinition pipelineDefinition, AsyncServiceScope scope)
        {
            foreach (var stepType in pipelineDefinition.Steps)
            {
                var step = scope.ServiceProvider.GetService(stepType) as IPipelineStep<T>;
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