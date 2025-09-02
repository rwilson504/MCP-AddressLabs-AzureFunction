using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AddressLabsMCP.Functions
{
    public class SwaggerFunction
    {
        private readonly ILogger<SwaggerFunction> _logger;

        public SwaggerFunction(ILogger<SwaggerFunction> logger)
        {
            _logger = logger;
        }

        [Function("SwaggerFunction")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger.json")] HttpRequestData req)
        {
            _logger.LogInformation("Serving the Swagger file.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "swagger.json");

            if (!File.Exists(filePath))
            {
                var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
                notFoundResponse.Headers.Add("Content-Type", "application/json");
                await notFoundResponse.WriteStringAsync("Swagger file not found.");
                return notFoundResponse;
            }

            var json = await File.ReadAllTextAsync(filePath);

            // Parse the JSON and replace the "host" field
            var swaggerDoc = JsonNode.Parse(json);
            if (swaggerDoc != null && swaggerDoc["host"] != null)
            {
                var host = req.Url.Host;
                var port = req.Url.Port;

                // Include the port if it's not a standard port (80 for HTTP, 443 for HTTPS)
                if (!(req.Url.Scheme == "http" && port == 80) && !(req.Url.Scheme == "https" && port == 443))
                {
                    host = $"{host}:{port}";
                }

                swaggerDoc["host"] = host;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteStringAsync(swaggerDoc?.ToJsonString() ?? json);

            return response;
        }
    }
}