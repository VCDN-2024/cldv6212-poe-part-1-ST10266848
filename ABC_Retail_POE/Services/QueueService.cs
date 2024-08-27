using Azure.Storage.Queues;
using System.Threading.Tasks;

public class QueueService
{
    private readonly QueueClient _queueClient;

    
    public QueueService(string connectionString, string queueName)
    {
        _queueClient = new QueueClient(connectionString, "notifications");
    }

    public async Task SendMessageAsync(string notification)
    {
        //notification = $"Processing Order {OrderId}";
        await _queueClient.SendMessageAsync(notification);
    }
}
