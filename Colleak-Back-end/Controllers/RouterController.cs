using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Colleak_Back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RouterController : ControllerBase
    {
        private readonly IRouterService _iRouterService;

        public RouterController(IRouterService iRouterService)
        {
            _iRouterService = iRouterService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            return "Hello world from RouterController!!";
        }

        [HttpGet("getrouterdata")]
        public async Task<ActionResult<List<DeviceInfo>>> GetRouterData()
        {
            return await _iRouterService.GetAllRouterInfo();
        }
    }    
}
