using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading;
using System.Threading.Tasks;

namespace Myquartznet
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;

        public QuartzHostedService(ILogger<QuartzHostedService> logger, IScheduler scheduler)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Quartz Schedule");
            return _scheduler.Start(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop Quartz Schedule");
            return _scheduler.Shutdown(cancellationToken);
        }
    }
}