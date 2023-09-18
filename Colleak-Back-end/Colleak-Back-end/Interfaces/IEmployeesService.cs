using Colleak_Back_end.Models;

namespace Colleak_Back_end.Interfaces
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetEmployeeAsync();

        Task<Employee?> GetEmployeeAsync(string id);

        Task CreateEmployeeAsync(Employee newEmployee);

        Task UpdateEmployeeAsync(string id, Employee updateEmployee);

        Task DeleteEmployeeAsync(string id);
    }
}
