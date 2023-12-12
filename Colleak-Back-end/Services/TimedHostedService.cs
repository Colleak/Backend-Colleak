using Colleak_Back_end.Controllers;
using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;

namespace Colleak_Back_end.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private double cooldownTimeInMinutes = 15;

        private readonly ILogger<TimedHostedService> _logger;
        private readonly RouterService _iRouterService;
        private readonly EmployeesService _iEmployeeService;

        private readonly UpdateRouterInfo _updateRouterInfo;

        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, RouterService iRouterService, EmployeesService iEmployeeService, UpdateRouterInfo updateRouterInfo)
        {
            _logger = logger;
            _iRouterService = iRouterService;
            _iEmployeeService = iEmployeeService;
            _updateRouterInfo = updateRouterInfo;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(cooldownTimeInMinutes));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);

            _updateRouterInfo.UpdateAllRecentDeviceNames(_iEmployeeService, _iRouterService);
        }        

        public Task StopAsync(CancellationToken cancellationToken)
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
