namespace NerdStore.Pagamentos.AntiCorruption.Gateway;

public class PagarmeGateway : IPagarmeGateway
{
    public string GetPagarmeServiceKey(string apiKey, string encryptionKey)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)])
            .ToArray());
    }

    public string GetCardHashKey(string serviceKey, string creditCard)
    {
        return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
            .Select(s => s[new Random().Next(s.Length)])
            .ToArray());
    }

    public bool CommitTransaction(string cardHasKey, string orderId, decimal amount)
    {
        Thread.Sleep(3000);
        return new Random().Next(2) == 0;
    }
}