using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colleak_Back_end.Controllers
{

    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _iEmployeesService;
        private readonly IRouterService _iRouterService;

        public EmployeesController(IEmployeesService iEmployeesService, IRouterService iRouterService) 
        {
            _iEmployeesService = iEmployeesService;
            _iRouterService = iRouterService;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            var employees = await _iEmployeesService.GetEmployeeAsync(); 
            
            return employees != null ? Ok(employees) : NotFound();
        }

        [HttpGet("locationtrackedEmployees")]
        public async Task<ActionResult<List<Employee>>> GetTrackedEmployees()
        {
            var employees = await _iEmployeesService.GetTrackedEmployeesAsync();

            return employees != null ? Ok(employees) : NotFound();
        }
        [HttpGet("connectedtodevice")]
        public async Task<ActionResult<List<Employee>>> GetConnectedToDeviceEmployees()
        {
            var employees = await _iEmployeesService.GetConnectedToDeviceEmployeesAsync();

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
        public async Task<ActionResult> Delete(string id, Employee deleteEmployee)
        {
            if (deleteEmployee is null || deleteEmployee.Id is null)
            {
                return BadRequest();
            }

            await _iEmployeesService.DeleteEmployeeAsync(deleteEmployee.Id);

            return Ok();
        }

        [HttpGet("Getrouterinfo")]
        public async Task<ActionResult<List<string>>> GetRouterInfo()
        {
            List<DeviceInfo> routerinfo = await _iRouterService.GetAllRouterInfo();

            if (routerinfo == null) return NoContent();

            List<string> macAddresses = new List<string>();
            foreach (DeviceInfo item in routerinfo)
            {
                if (item.Mac != null)
                {
                    macAddresses.Add(item.Mac);
                }
            }
            return macAddresses;
        }

        [HttpPut("{id:length(24)}/mac")]
        public async Task<ActionResult<string>> UpdateMacAddress(string id, string macAddress)
        {
            if (null == id) return BadRequest();            
            var employee = await _iEmployeesService.GetEmployeeAsync(id);
            if (employee is null) return NotFound();            

            if (employee.ConnectedDeviceMacAddress == macAddress) return Ok();

            employee.ConnectedDeviceMacAddress = macAddress;
            employee.ConnectedToDevice = true;
            await _iEmployeesService.UpdateEmployeeAsync(id, employee);

            return Ok();
        }    
        

    }
}
