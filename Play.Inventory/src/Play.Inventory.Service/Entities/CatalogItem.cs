using Play.Common.Entities;

namespace Play.Inventory.Service.Entities;

public class CatalogItem : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}