using Microsoft.Extensions.Logging;
using NCrontab;

namespace UsingCrontab
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var everydayAt11Pm= "0 * * * *";
            var cronSchedule = CrontabSchedule.Parse(everydayAt11Pm);

            DateTime nextOccurrence = cronSchedule.GetNextOccurrence(DateTime.Now);

            Console.WriteLine(nextOccurrence);


            var timehostedService = new TimedHostedService(new ConsoleLogger());

           await timehostedService.StartAsync(CancellationToken.None);

            Console.ReadLine();

            Console.WriteLine("Removed time hosted service");
            await timehostedService.StopAsync(CancellationToken.None);
            Console.ReadLine();
        }

    }

    public class ConsoleLogger : ILogger<TimedHostedService>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(formatter(state, exception));
        }
    }
}
