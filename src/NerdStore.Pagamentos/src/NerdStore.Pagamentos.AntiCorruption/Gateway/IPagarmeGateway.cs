namespace NerdStore.Pagamentos.AntiCorruption.Gateway;

public interface IPagarmeGateway
{
    string GetPagarmeServiceKey(string apiKey, string encryptionKey);
    string GetCardHashKey(string serviceKey, string creditCard);
    bool CommitTransaction(string cardHasKey, string orderId, decimal amount);
}