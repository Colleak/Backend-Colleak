using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;

namespace Colleak_Back_end.Services
{
    public class UpdateRouterInfo
    {

        public UpdateRouterInfo() 
        {

        }

        public async void UpdateAllRecentDeviceNames(EmployeesService _iEmployeeService, RouterService _iRouterService, double timeCooldownInMinutes)
        {
            List<Employee> employees = await _iEmployeeService.GetEmployeeAsync();
            List<DeviceInfo> routerData = await _iRouterService.GetAllRouterInfo();

            foreach (Employee employee in employees)
            {
                foreach (DeviceInfo deviceInfo in routerData)
                {
                    if (employee.ConnectedDeviceMacAddress == deviceInfo.Mac)
                    {
                        Console.WriteLine(employee.ConnectedRouterName);
                        if (!CheckIfSeenToday(timeCooldownInMinutes, deviceInfo.LastSeen)) break;
                        employee.ConnectedRouterName = deviceInfo.RecentDeviceName;
                        Console.WriteLine(employee.ConnectedRouterName);

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
    }
}
