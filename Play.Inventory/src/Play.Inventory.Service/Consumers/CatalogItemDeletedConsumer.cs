using MassTransit;
using Play.Common.Repositories;
using Play.Inventory.Service.Entities;
using static Play.Catalog.Contracts.Contracts;

namespace Play.Inventory.Service.Consumers;

public class CatalogItemDeletedConsumer(IRepository<CatalogItem> repository) : IConsumer<CatalogItemDeleted>
{
    private readonly IRepository<CatalogItem> _repository = repository;

    public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
    {
        var message = context.Message;
        var item = await _repository.GetByIdAsync(message.ItemId);
        if (item == null) return;

        await _repository.RemoveAsync(message.ItemId);
    }
}