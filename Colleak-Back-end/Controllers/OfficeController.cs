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

        private readonly MockMessage defaultMessage = new MockMessage("SN", "RN", "SI", "RI", "Default message", 11, "true");

        public OfficeController(IEmployeesService iEmployeesService)
        {
            _iEmployeesService = iEmployeesService;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            return "Hello wordl!!";
        }

        [HttpPost("sendmessage")]
        public async Task<ActionResult> PostSendMessage(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            usedMockMessage.sender_name = GetName(data.sender_id).ToString();
            usedMockMessage.receiver_name = GetName(data.receiver_id).ToString();
            usedMockMessage.message = data.message;

            var mockAPIUrl = mockAPIAdress + "/send_message";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("sendping")]
        public async Task<ActionResult> PostSendPing(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            usedMockMessage.sender_name = GetName(data.sender_id).ToString();
            usedMockMessage.receiver_name = GetName(data.receiver_id).ToString();

            var mockAPIUrl = mockAPIAdress + "/send_ping";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("sendcall")]
        public async Task<ActionResult> PostSendCall(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            usedMockMessage.sender_name = GetName(data.sender_id).ToString();
            usedMockMessage.receiver_name = GetName(data.receiver_id).ToString();

            var mockAPIUrl = mockAPIAdress + "/send_call";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }

        [HttpPost("available")]
        public async Task<ActionResult> PostAvailable(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            usedMockMessage.sender_name = GetName(data.sender_id).ToString();
            usedMockMessage.receiver_name = GetName(data.receiver_id).ToString();

            var mockAPIUrl = mockAPIAdress + "/available";

            var content = CreateStringContent(usedMockMessage);

            return await PostRequest(mockAPIUrl, content);
        }
        [HttpPost("disturb")]
        public async Task<ActionResult> PostDisturb(MockMessage data)
        {
            MockMessage usedMockMessage = defaultMessage;

            usedMockMessage.sender_name = GetName(data.sender_id).ToString();
            usedMockMessage.receiver_name = GetName(data.receiver_id).ToString();

            var mockAPIUrl = mockAPIAdress + "/disturb";

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

        private async Task<string?> GetName(string id)
        {
            var result = await _iEmployeesService.GetEmployeeAsync(id);

            if (result == null)
            {
                return "No name found";
            }

            return result.EmployeeName;
        }

        private StringContent CreateStringContent(MockMessage data)
        {
            var jsonContent = JsonConvert.SerializeObject(data);
            return new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }
    }
}
