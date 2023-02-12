namespace NerdStore.Catalogo.Domain.Services;

public interface IStockService: IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> AddStock(Guid productId, int quantity);
}