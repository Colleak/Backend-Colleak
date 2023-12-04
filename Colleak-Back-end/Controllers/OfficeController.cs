using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Colleak_Back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly string mockAPIAdress = "http://artofghost.pythonanywhere.com";

        private readonly IEmployeesService _iEmployeesService;

        private readonly MockMessage defaultMessage = new MockMessage("SN", "RN", "652551bdb82d091daccff161", "652551bdb82d091daccff161", "Default message", 
            11/*verander dit naar 10 om een andere response te krijgen*/, "true", "true");

        public OfficeController(IEmployeesService iEmployeesService)
        {
            _iEmployeesService = iEmployeesService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            return "Hello world!!";
        }

        [HttpPost("sendmessage")]
        public async Task<ActionResult> PostSendMessage(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;
            usedMockMessage.message = data.message;

            var mockAPIUrl = mockAPIAdress + "/send_message";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("sendping")]
        public async Task<ActionResult> PostSendPing(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;

            var mockAPIUrl = mockAPIAdress + "/send_ping";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("sendcall")]
        public async Task<ActionResult> PostSendCall(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;

            var mockAPIUrl = mockAPIAdress + "/send_call";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("on_location")]
        public async Task<ActionResult> GetOn_Location(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/on_location";

            var content = CreateStringContent(data);

            return await PostRequest(mockAPIUrl, content);
        }
        [HttpPost("disturb")]
        public async Task<ActionResult> PostDisturb(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;

            var mockAPIUrl = mockAPIAdress + "/disturb";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }
        [HttpPost("set_location")]
        public async Task<ActionResult> PostSetLocation(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/set_location";

            var content = CreateStringContent(data);

            return await PostRequest(mockAPIUrl, content);
        }
        [HttpPost("available")]
        public async Task<ActionResult> PostAvailable(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;

            var mockAPIUrl = mockAPIAdress + "/available";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }
        [HttpPost("atm_available")]
        public async Task<ActionResult> PostATMAvailable(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            Task<Employee> sender = GetName(data.sender_id);
            Task<Employee> receiver = GetName(data.receiver_id);

            usedMockMessage.sender_name = sender.Result.EmployeeName;
            usedMockMessage.receiver_name = receiver.Result.EmployeeName;

            var mockAPIUrl = mockAPIAdress + "/atm_available";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        private async Task<ActionResult> PostRequest(string mockAPIUrl, StringContent content)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.PostAsync(mockAPIUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Optionally deserialize if you need to process the response
                    // var responseData = JsonConvert.DeserializeObject<YourResponseType>(responseContent);
                    return Ok(responseContent);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error occurred during the API call");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception details
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }        
        private Task<Employee?> GetName(string id)
        {
            Task<Employee?> result = _iEmployeesService.GetEmployeeAsync(id);

            return result;
        }

        private StringContent CreateStringContent(MockMessage data)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }
    }
}
