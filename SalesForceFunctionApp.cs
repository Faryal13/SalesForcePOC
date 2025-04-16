using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SalesForcePOC;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


public class SalesForceFunctionApp
{
    private readonly SalesforceClient _salesforceClient;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly ILogger _logger;
    private readonly string _jobTimmer;

    public SalesForceFunctionApp(SalesforceClient salesforceClient, ServiceBusSender serviceBusSender, ILoggerFactory loggerFactory, IConfiguration config)
    {
        _salesforceClient = salesforceClient;
        _serviceBusSender = serviceBusSender;
        _logger = loggerFactory.CreateLogger<SalesForceFunctionApp>();
        _jobTimmer = config["FunctionTriggerSchedule"];

    }

    [Function("SalesforceTimerTrigger")]
    public async Task Run([Microsoft.Azure.Functions.Worker.TimerTrigger("%FunctionTriggerSchedule%")] Microsoft.Azure.Functions.Worker.TimerInfo myTimer)
    {
        _logger.LogInformation($"Function executed at: {DateTime.Now}");

        var jsonData = await _salesforceClient.GetSalesforceDataAsync();
        await _serviceBusSender.SendMessageAsync(jsonData);
    }
}
