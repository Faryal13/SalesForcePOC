using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class ServiceBusSender
{
    private readonly string _connectionString;
    private readonly string _queueName;

    public ServiceBusSender(IConfiguration config)
    {
        _connectionString = config["ServiceBusConnectionString"];
        _queueName = config["ServiceBusQUeueURL"];
    }

    public async Task SendMessageAsync(string message)
    {
        var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(_queueName);

        try
        {
            await sender.SendMessageAsync(new ServiceBusMessage(message));
        }
        finally
        {
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }
    }

}
