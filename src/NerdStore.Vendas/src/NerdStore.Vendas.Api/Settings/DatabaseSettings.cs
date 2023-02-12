namespace NerdStore.Vendas.Api.Settings;

public class DatabaseSettings
{
    public bool InMemory { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
}