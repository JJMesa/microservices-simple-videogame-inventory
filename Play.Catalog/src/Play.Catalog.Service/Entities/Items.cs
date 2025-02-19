using Play.Common.Entities;

namespace Play.Catalog.Service.Entities;

public class Item(Guid id, string name, string description, decimal price, DateTimeOffset createdDate) : IEntity
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public DateTimeOffset CreatedDate { get; set; } = createdDate;
}