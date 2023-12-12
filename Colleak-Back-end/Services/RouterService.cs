using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Colleak_Back_end.Models;
using Newtonsoft.Json;
using Colleak_Back_end.Interfaces;

namespace Colleak_Back_end.Services
{
    public class RouterService : IRouterService
    {
        public async Task<List<DeviceInfo>>GetAllRouterInfo()
        {
            SecretClient client = new SecretClient(new Uri("https://colleakdatabase.vault.azure.net/"), new DefaultAzureCredential());
            KeyVaultSecret merakiKey = client.GetSecret("X-Cisco-Meraki-API-Key");

            string apiUrl = "https://api.meraki.com/api/v1/networks/L_714946440845072792/clients?timespan=900&perPage=5000";

            HttpClient httpClient = new HttpClient();

            // Add headers if needed
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 75dd5334bef4d2bc96f26138c163c0a3fa0b5ca6");            
            httpClient.DefaultRequestHeaders.Add("X-Cisco-Meraki-API-Key", merakiKey.Value);

            try
            {
                // Send the GET request
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read and display the response content
                    string content = await response.Content.ReadAsStringAsync();
                    List<DeviceInfo> myObjects = JsonConvert.DeserializeObject<List<DeviceInfo>>(content);

                    return myObjects;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<DeviceInfo>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<DeviceInfo>();
            }
        }
    }
}
