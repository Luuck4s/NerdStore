using NerdStore.Catalogo.Domain.Entities;
using NerdStore.Core.Dtos;

namespace NerdStore.Catalogo.Domain.Services;

public interface IStockService: IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> AddStock(Product product, int quantity);
    Task<bool> DebitListItemStock(ItemOrderListDto itemsOrder);
    Task<bool> AddListItemStock(ItemOrderListDto itemsOrder);
}