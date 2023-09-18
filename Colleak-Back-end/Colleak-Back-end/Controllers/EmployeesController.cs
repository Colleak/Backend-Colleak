using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colleak_Back_end.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _iEmployeesService;

        public EmployeesController(IEmployeesService iEmployeesService) =>
        _iEmployeesService = iEmployeesService;

        [HttpGet]
        public async Task<List<Employee>> Get() =>
            await _iEmployeesService.GetEmployeeAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            var employee = await _iEmployeesService.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee newEmployee)
        {
            await _iEmployeesService.CreateEmployeeAsync(newEmployee);

            return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Employee updatedEmployee)
        {
            var employee = await _iEmployeesService.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            updatedEmployee.Id = employee.Id;

            await _iEmployeesService.UpdateEmployeeAsync(id, updatedEmployee);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _iEmployeesService.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            await _iEmployeesService.DeleteEmployeeAsync(id);

            return NoContent();
        }
    }
}
