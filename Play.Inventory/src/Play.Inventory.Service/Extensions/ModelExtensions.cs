using Play.Inventory.Service.Dtos;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Extensions;

public static class ModelExtensions
{
    public static InventoryItemDto AsDto(this InventoryItem inventoryItem, string name, string description) =>
        new(inventoryItem.CatalogItemId, name, description, inventoryItem.Quantity, inventoryItem.AcquiredDate);

    public static InventoryItem AsEntity(this GrantItemsDto grantItemsDto) =>
        new()
        {
            UserId = grantItemsDto.UserId,
            CatalogItemId = grantItemsDto.CatalogItemId,
            Quantity = grantItemsDto.Quantity,
            AcquiredDate = DateTimeOffset.UtcNow
        };
}