using Colleak_Back_end.Models;

namespace Colleak_Back_end.Interfaces
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetEmployeeAsync();
        Task<List<Employee>> GetTrackedEmployeesAsync();
        Task<List<Employee>> GetUntrackedEmployeesAsync();
        Task<List<List<Employee>>> GetTrackedAndUntrackedEmployeesAsync();
        Task<List<Employee>> GetConnectedToDeviceEmployeesAsync();

        Task<Employee?> GetEmployeeAsync(string id);

        Task CreateEmployeeAsync(Employee newEmployee);

        Task UpdateEmployeeAsync(string id, Employee updateEmployee);

        Task DeleteEmployeeAsync(string id);
    }
}
