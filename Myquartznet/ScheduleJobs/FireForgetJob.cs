using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace Myquartznet.ScheduleJobs
{
    public sealed class FireForgetJob : IJob
    {
        private readonly ILogger _logger;

        public FireForgetJob(ILogger<FireForgetJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("This is a Fire and Forget job!");
            return Task.CompletedTask;
        }

        public Task StartJob(IScheduler scheduler)
        {
            //use JobBuilder to Create a jobDetail
            var jobDetails = JobBuilder
                .CreateForAsync<FireForgetJob>()
                .WithIdentity("FireForgetJob")
                .WithDescription("My First Fire and Forget job")
                .Build();

            //use TriggerBuilder to create a Trigger
            var trigger = TriggerBuilder
                .Create()
                .StartNow()
                .Build();

            //call the scheduler.ScheduleJob
            return scheduler.ScheduleJob(jobDetails, trigger);
        }
    }
}