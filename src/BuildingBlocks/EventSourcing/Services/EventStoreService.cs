
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing.Services;

public class EventStoreService: IEventStoreService
{
    private readonly IEventStoreConnection _connection;

    public EventStoreService(IConfiguration configuration)
    {
        var connectionSettings = ConnectionSettings.Create();
        connectionSettings.DisableServerCertificateValidation();
        connectionSettings.DisableTls();
        connectionSettings.EnableVerboseLogging()
            .UseDebugLogger()
            .UseConsoleLogger()
            .SetHeartbeatTimeout(TimeSpan.FromSeconds(60))
            .SetHeartbeatInterval(TimeSpan.FromSeconds(30));

        
        var connectionString = configuration.GetSection("EventStoreSettings")["ConnectionString"];
        _connection = EventStoreConnection.Create(
            connectionString,
            connectionSettings, "MyConName");
        _connection.ConnectAsync();
    }

    public IEventStoreConnection GetConnection()
    {
        return _connection;
    }
}