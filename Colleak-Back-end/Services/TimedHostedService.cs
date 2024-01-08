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

        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, RouterService iRouterService, EmployeesService iEmployeeService)
        {
            _logger = logger;
            _iRouterService = iRouterService;
            _iEmployeeService = iEmployeeService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(cooldownTimeInMinutes));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);

            UpdateAllRecentDeviceNames(cooldownTimeInMinutes);
        }

        public async void UpdateAllRecentDeviceNames(double timeCooldownInMinutes)
        {
            List<Employee> employees = await _iEmployeeService.GetEmployeeAsync();
            List<DeviceInfo> routerData = await _iRouterService.GetAllRouterInfo();

            foreach (Employee employee in employees)
            {
                foreach (DeviceInfo deviceInfo in routerData)
                {
                    if (employee.ConnectedDeviceMacAddress == deviceInfo.Mac)
                    {
                        Console.WriteLine("saved router name" + employee.ConnectedRouterName);
                        if (!CheckIfSeenToday(timeCooldownInMinutes, deviceInfo.LastSeen)) break;
                        if (employee.ConnectedRouterName == deviceInfo.RecentDeviceName) break;
                        
                        employee.ConnectedRouterName = deviceInfo.RecentDeviceName;
                        Console.WriteLine("new saved router name" + employee.ConnectedRouterName);

                        await _iEmployeeService.UpdateEmployeeAsync(employee.Id, employee);
                        break;
                    }
                }
            }
        }
        private bool CheckIfSeenToday(double timeCooldownInMinutes, DateTime LastSeen)
        {
            if (DateTime.UtcNow.Day != LastSeen.Day)
            {
                return false;
            }
            return true;
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
