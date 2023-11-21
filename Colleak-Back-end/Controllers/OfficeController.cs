using Colleak_Back_end.Interfaces;
using Colleak_Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Colleak_Back_end.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly string mockAPIAdress = "http://localhost:8001";
        private readonly IHttpClientFactory _clientFactory;

        public OfficeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpPost("/sendmessage")]
        public async Task<IActionResult> PostSendMessage(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/send_message";
            var client = _clientFactory.CreateClient();

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

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




            //[HttpPost("sendping")]
            //public async Task<ActionResult> PostSendPing()
            //{

            //}

            //[HttpPost("available")]
            //public async Task<ActionResult> PostAvailable()
            //{

            //}

            //[HttpPost("disturb")]
            //public async Task<ActionResult> PostDisturb()
            //{

            //}
        }
    }
}
