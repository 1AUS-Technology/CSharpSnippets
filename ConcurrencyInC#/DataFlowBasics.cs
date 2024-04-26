using System.Formats.Asn1;
using System.Threading.Tasks.Dataflow;

namespace ConcurrencyInC_;

public class DataFlowBasics
{
    public static async Task LinkBlocks()
    {
        // execute in parallel
        var multiplyBlock = new TransformBlock<int, int>(
            item => item * 2,
            new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded
            });
       
        var substractBlock = new TransformBlock<int, int>(x => x - 2);

        var options = new DataflowLinkOptions { PropagateCompletion = true };
        multiplyBlock.LinkTo(substractBlock, options);

       
       
        await substractBlock.Completion;
    }
}