namespace NerdStore.Core.Dtos;

public record ItemOrderListDto(Guid OrderId, ICollection<ItemOrderDto> Items);