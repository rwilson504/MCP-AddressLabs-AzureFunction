using System.Text.Json;
using System.Net.Http.Json;
using AddressLabsMCP.Models;
using Microsoft.Extensions.Logging;

namespace AddressLabsMCP.Services
{
    public class AddressLabsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddressLabsService> _logger;
        public AddressLabsService(HttpClient httpClient,ILogger<AddressLabsService> logger)
        {
            _logger = logger;
            _logger.LogInformation("AddressLabsService initialized");
            _httpClient = httpClient;
        }

        public async Task<object> ParseAddressAsync(string address)
        {
            var requestBody = new { address };
            var response = await _httpClient.PostAsJsonAsync("http://api.addresslabs.com/v1/parsed-address", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to parse address. Status Code: {StatusCode}, Error: {ErrorContent}", response.StatusCode, errorContent);
                return new ErrorResponse($"Failed to parse address: {errorContent}", (int)response.StatusCode);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonSerializer.Deserialize<ParsedAddressResponse>(responseContent);
            if (parsedResponse == null)
            {
                return new ErrorResponse("Deserialization returned null.", 500);
            }
            return parsedResponse;
        }
    }
}