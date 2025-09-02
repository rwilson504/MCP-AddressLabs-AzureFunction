using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.Logging;
using AddressLabsMCP.Services;
using static AddressLabsMCP.Tools.ToolsInformation;

namespace AddressLabsMCP.Tools
{
    public class AddressLabsTool
    {
        private readonly AddressLabsService _service;
        private readonly ILogger<AddressLabsTool> _logger;

        public AddressLabsTool(AddressLabsService service, ILogger<AddressLabsTool> logger)
        {
            _service = service;
            _logger = logger;
        }
        
        [Function(nameof(ParseAddress))]
               public async Task<object> ParseAddress(
            [McpToolTrigger(ParseAddressToolName, ParseAddressToolDescription)]
                ToolInvocationContext context,
            [McpToolProperty(AddressPropertyName, AddressPropertyType, AddressPropertyDescription)]
                string address)                
        {
            _logger.LogInformation("Received address to parse: {Address}", address);            
            // Call the AddressLabsService to parse the address
            var result = await _service.ParseAddressAsync(address);
            _logger.LogInformation("Parsed address result: {Result}", result);
            return result;            
        }
    }
}