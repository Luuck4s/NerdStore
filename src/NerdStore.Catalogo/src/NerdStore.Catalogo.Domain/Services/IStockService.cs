namespace NerdStore.Catalogo.Domain.Services;

public interface IStockService: IDisposable
{
    Task<bool> DebitStock(Guid produtoId, int quantidade);
    Task<bool> AddStock(Guid produtoId, int quantidade);
}