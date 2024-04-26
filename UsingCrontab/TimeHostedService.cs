using Microsoft.Extensions.Logging;
using NCrontab;


namespace UsingCrontab
{
    public class TimedHostedService :  IDisposable
    {
        private const string EverydayAt11Pm = "0 23 15 */2 *";
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private Timer _timer;
        private CrontabSchedule cronSchedule;
        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
            cronSchedule = CrontabSchedule.Parse(EverydayAt11Pm);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");
            
            DateTime nextOccurrence = cronSchedule.GetNextOccurrence(DateTime.Now);
            var totalSeconds = (nextOccurrence - DateTime.Now).TotalSeconds;
            
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(totalSeconds) , TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}