using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SalesForcePOC;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults() // No AddTimers() needed!
    .ConfigureServices(services =>
    {
        services.AddSingleton<SalesforceClient>();
        services.AddSingleton<ServiceBusSender>();
        services.AddHttpClient<SalesforceClient>();

    })
    .Build();

host.Run();
