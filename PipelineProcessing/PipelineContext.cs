using System.Collections.Concurrent;

namespace QR.Cii.CommonScheduleProcessor.Core.PipelineProcessing
{
    public class PipelineContext
    {
        public ConcurrentDictionary<string, object> Parameters { get; } = new();
        public bool CancelExecution { get; set; }
    }
}