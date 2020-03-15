using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Myquartznet.ScheduleJobs
{
    public class RecurringJob : IJob
    {
        private readonly ILogger _logger;

        public RecurringJob(ILogger<RecurringJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation(DateTime.UtcNow.ToString());
            _logger.LogInformation("This is a Recurring job!");
            return Task.CompletedTask;
        }

        public Task StartJob(IScheduler scheduler)
        {
            //use JobBuilder to Create a jobDetail
            var jobDetails = JobBuilder
                .CreateForAsync<RecurringJob>()
                .WithIdentity("RecurringJob")
                .WithDescription("My First Recurring job")
                .Build();

            //use TriggerBuilder to create a Trigger
            var trigger = TriggerBuilder
                        .Create()
                        .StartNow()
                        .WithSimpleSchedule(builder =>
                            builder.WithIntervalInSeconds(3)// start a job every 3 seconds
                                .RepeatForever()
                        )
                        .Build();

            //call the scheduler.ScheduleJob
            return scheduler.ScheduleJob(jobDetails, trigger);
        }
    }
}