﻿using NerdStore.Core.Events.IntegrationEvents.Order.DTOs;

namespace NerdStore.Catalogo.Domain.Services;

public interface IStockService: IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> DebitListItemStock(ItemOrderListDto products);
    Task<bool> AddStock(Guid productId, int quantity);
}