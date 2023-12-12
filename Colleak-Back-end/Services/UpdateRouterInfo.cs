using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;

namespace Colleak_Back_end.Services
{
    public class UpdateRouterInfo
    {

        public UpdateRouterInfo() 
        {

        }

        public async void UpdateAllRecentDeviceNames(EmployeesService _iEmployeeService, RouterService _iRouterService)
        {
            List<Employee> employees = await _iEmployeeService.GetEmployeeAsync();
            List<DeviceInfo> routerData = await _iRouterService.GetAllRouterInfo();

            foreach (Employee employee in employees)
            {
                Employee updatedEmployee = LoopDeviceInfo(employee, routerData);

                await _iEmployeeService.UpdateEmployeeAsync(updatedEmployee.Id, updatedEmployee);
            }
        }

        private Employee LoopDeviceInfo(Employee employee, List<DeviceInfo> routerData)
        {
            foreach (DeviceInfo deviceInfo in routerData)
            {
                if (employee.ConnectedDeviceMacAddress == deviceInfo.Mac)
                {
                    employee.ConnectedRouterName = deviceInfo.RecentDeviceName;
                    Console.WriteLine(employee.ConnectedRouterName);
                    return employee;
                }
            }
            return employee;
        }
    }
}
