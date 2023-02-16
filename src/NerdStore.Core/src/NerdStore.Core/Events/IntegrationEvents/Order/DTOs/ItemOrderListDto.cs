namespace NerdStore.Core.Events.IntegrationEvents.Order.DTOs;

public record ItemOrderListDto(Guid OrderId, ICollection<ItemOrderDto> Items);