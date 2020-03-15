using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace Myquartznet
{
    public class MyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public MyJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        => ActivatorUtilities.CreateInstance(_serviceProvider, bundle.JobDetail.JobType) as IJob;

        public void ReturnJob(IJob job)
        {
            if (job is IDisposable disposableJob)
            {
                disposableJob.Dispose();
            }
        }
    }
}