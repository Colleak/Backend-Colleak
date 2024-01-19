using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Colleak_Back_end.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

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
        [HttpGet("trackedAndUntrackedEmployees")]
        public async Task<ActionResult<List<List<Employee>>>> GetTrackedAndUntrackedEmployees()
        {
            var employees = await _iEmployeesService.GetTrackedAndUntrackedEmployeesAsync();

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

        [HttpPost("employee/login")]
        public async Task<ActionResult> PostEmployeeIp(Employee newEmployee)
        {
            if (newEmployee.EmployeeName == null || newEmployee.ip == null)
            {
                return BadRequest();
            }

            List <Employee> employees = await _iEmployeesService.GetEmployeeAsync();
            foreach (Employee employee in employees)
            {
                if (employee.EmployeeName == newEmployee.EmployeeName)
                {
                    return BadRequest("Employee already exist with the send name");
                }
            }

            foreach (DeviceInfo user in _iRouterService.GetAllRouterInfo().Result)
            {
                if (user.Ip == newEmployee.ip)
                {
                    newEmployee.ConnectedDeviceMacAddress = user.Mac;
                    newEmployee.ConnectedRouterName = user.RecentDeviceName;
                    newEmployee.ConnectedToDevice = true;
                    await _iEmployeesService.CreateEmployeeAsync(newEmployee);
                    return Ok(CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee));
                }
            }

            return BadRequest("Fuck you");
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

        [HttpPut("{id:length(24)}/ip")]
        public async Task<ActionResult<string>> UpdateIpAddress(string id, string ipAddress)
        {
            Employee employee = validIdAndUser(id).Result;
            if (employee == null) return BadRequest("Id is not valid or the employee does not exist");
            if (ipAddress == null) return BadRequest("ipAddress can not be null");

            if (employee.ConnectedDeviceMacAddress != null) return Ok("user already has a macAddress");

            foreach (DeviceInfo user in _iRouterService.GetAllRouterInfo().Result)
            {
                if (user.Ip == ipAddress)
                {
                    employee.ConnectedDeviceMacAddress = user.Mac;
                    employee.ConnectedRouterName = user.RecentDeviceName;
                    employee.ConnectedToDevice = true;
                    await _iEmployeesService.UpdateEmployeeAsync(id, employee);
                    return Ok("user has new macAddress");
                }
            }           

            return NotFound("user can not be found in the router info");
        }

        [HttpPut("{id:length(24)}/iphotfix")]
        public async Task<ActionResult<string>> UpdateIpAddresshotfix(string id, Employee updatedEmployee)
        {
            Employee employee = validIdAndUser(id).Result;
            if (employee == null) return BadRequest("Id is not valid or the employee does not exist");
            if (updatedEmployee.ip == null) return BadRequest("ipAddress can not be null");

            if (employee.ConnectedDeviceMacAddress != null) return Ok("user already has a macAddress");

            foreach (DeviceInfo user in _iRouterService.GetAllRouterInfo().Result)
            {
                if (user.Ip == updatedEmployee.ip)
                {
                    employee.ConnectedDeviceMacAddress = user.Mac;
                    employee.ConnectedRouterName = user.RecentDeviceName;
                    employee.ConnectedToDevice = true;
                    await _iEmployeesService.UpdateEmployeeAsync(id, employee);
                    return Ok("user has new macAddress");
                }
            }

            return NotFound("user can not be found in the router info");
        }

        [HttpPut("{id:length(24)}/updateMac")]
        public async Task<ActionResult<string>> UpdateMacAddress(string id, string ipAddress)
        {
            Employee employee = validIdAndUser(id).Result;
            if (employee == null) return BadRequest("Id is not valid or the employee does not exist");
            if (ipAddress == null) return BadRequest("ipAddress can not be null");

            foreach (DeviceInfo user in _iRouterService.GetAllRouterInfo().Result)
            {
                if (user.Ip == ipAddress)
                {
                    employee.ConnectedRouterName = user.RecentDeviceName;
                    employee.ConnectedToDevice = true;

                    if (employee.ConnectedDeviceMacAddress == user.Mac) return Ok("mac is the same");

                    employee.ConnectedDeviceMacAddress = user.Mac;
                    await _iEmployeesService.UpdateEmployeeAsync(id, employee);
                    return Ok("user has new macAddress");
                }
            }
            return NotFound("user can not be found in the router info");
        }

        [HttpPut("{id:length(24)}/updaterecentDevice")]
        public async Task<ActionResult<string>> UpdateRecentDevice(string id)
        {
            Employee employee = validIdAndUser(id).Result;
            if (employee == null) return BadRequest("Id is not valid or the employee does not exist");

            foreach (DeviceInfo user in _iRouterService.GetAllRouterInfo().Result)
            {
                if (user.Mac == employee.ConnectedDeviceMacAddress)
                {
                    if (employee.ConnectedRouterName == user.RecentDeviceName) return Ok("connectedRouterName has not been changed");

                    employee.ConnectedRouterName = user.RecentDeviceName;

                    await _iEmployeesService.UpdateEmployeeAsync(id, employee);
                    return Ok("user has new recentDeviceName");
                }
            }
            return NotFound("user can not be found in the router info");
        }

        private async Task<Employee> validIdAndUser(string id)
        {
            if (null == id) return null;
            var employee = await _iEmployeesService.GetEmployeeAsync(id);
            if (employee is null) return null;
            return employee;
        }

    }
}
