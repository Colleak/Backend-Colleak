using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colleak_Back_end.Controllers
{

    [Route("/")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _iEmployeesService;

        public EmployeesController(IEmployeesService iEmployeesService) =>
        _iEmployeesService = iEmployeesService;

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            var employees = await _iEmployeesService.GetEmployeeAsync(); 
            
            return employees != null ? Ok(employees) : NotFound();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = await _iEmployeesService.GetEmployeeAsync(id);

            if (employee is null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Employee newEmployee)
        {
            if (!ModelState.IsValid || newEmployee.EmployeeName == null)
            {
                return BadRequest();
            }
            await _iEmployeesService.CreateEmployeeAsync(newEmployee);

            return Ok(CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Employee updatedEmployee)
        {
            if (id is null || updatedEmployee is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid || updatedEmployee.EmployeeName == null)
            {
                return BadRequest();
            }

            updatedEmployee.Id = id;

            await _iEmployeesService.UpdateEmployeeAsync(id, updatedEmployee);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(Employee deleteEmployee)
        {
            if (deleteEmployee is null || deleteEmployee.Id is null)
            {
                return BadRequest();
            }

            await _iEmployeesService.DeleteEmployeeAsync(deleteEmployee.Id);

            return Ok();
        }
    }
}
