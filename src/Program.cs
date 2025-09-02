using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using static AddressLabsMCP.Tools.ToolsInformation;
using AddressLabsMCP.Services;

var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();
builder.EnableMcpToolMetadata();
builder
    .ConfigureMcpTool(ParseAddressToolName)
    .WithProperty(AddressPropertyName, AddressPropertyType, AddressPropertyDescription);    
builder.Services.AddHttpClient<AddressLabsService>();

builder.Build().Run();