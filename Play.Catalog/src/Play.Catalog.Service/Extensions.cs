using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service;

public static class Extensions
{
    public static ItemDto AsDto(this Item item) =>
        new(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);

    public static Item AsEntity(this CreateItemDto createItemDto) =>
        new(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
}