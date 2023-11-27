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
        private readonly IHttpClientFactory _clientFactory;

        public OfficeController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetHelloWorld()
        {
            return "Hello wordl!!";
        }

        [HttpPost("sendmessage")]
        public async Task<ActionResult> PostSendMessage(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/send_message";
            var client = new HttpClient();

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
        }

        [HttpPost("sendping")]
        public async Task<ActionResult> PostSendPing(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/send_ping";
            var client = new HttpClient();

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
        }

        [HttpPost("available")]
        public async Task<ActionResult> PostAvailable(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/available";
            var client = new HttpClient();

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
        }
        [HttpPost("disturb")]
        public async Task<ActionResult> PostDisturb(MockMessage data)
        {
            var mockAPIUrl = mockAPIAdress + "/disturb";
            var client = new HttpClient();

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
        }
    }
}
