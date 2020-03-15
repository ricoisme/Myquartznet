using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Myquartznet.Models;
using Myquartznet.ScheduleJobs;
using Quartz;
using System.Diagnostics;

namespace Myquartznet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScheduler _scheduler;
        private readonly FireForgetJob _fireForgetJob;
        private readonly DelayJob _delayJob;
        private readonly RecurringJob _recurringJob;

        public HomeController(ILogger<HomeController> logger,
            IScheduler scheduler,
            FireForgetJob fireForgetJob,
            DelayJob delayJob,
            RecurringJob recurringJob)
        {
            _logger = logger;
            _scheduler = scheduler;
            _fireForgetJob = fireForgetJob;
            _delayJob = delayJob;
            _recurringJob = recurringJob;
        }

        public IActionResult Index()
        {
            _fireForgetJob.StartJob(_scheduler);
            _delayJob.StartJob(_scheduler);
            _recurringJob.StartJob(_scheduler);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}