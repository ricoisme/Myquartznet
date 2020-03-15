using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Myquartznet.ScheduleJobs
{
    public class DelayJob : IJob
    {
        private readonly ILogger _logger;

        public DelayJob(ILogger<DelayJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation(DateTime.UtcNow.ToString());
            _logger.LogInformation("This is a Delay job!");
            return Task.CompletedTask;
        }

        public Task StartJob(IScheduler scheduler)
        {
            //use JobBuilder to Create a jobDetail
            var jobDetails = JobBuilder
                .CreateForAsync<DelayJob>()
                .WithIdentity("DelayJob")
                .WithDescription("My First Delay job")
                .Build();

            //use TriggerBuilder to create a Trigger
            var trigger = TriggerBuilder
                        .Create()
                        .StartAt(DateTimeOffset.Now.AddSeconds(5))// start a job after 5 seconds
                        .Build();

            //call the scheduler.ScheduleJob
            return scheduler.ScheduleJob(jobDetails, trigger);
        }
    }
}