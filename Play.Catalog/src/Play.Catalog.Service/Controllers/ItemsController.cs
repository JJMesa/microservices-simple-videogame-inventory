using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Common.Repositories;
using static Play.Catalog.Contracts.Contracts;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("items")]
public class ItemsControllers(IRepository<Item> itemRepository, IPublishEndpoint publishEndpoint) : ControllerBase
{
    private readonly IRepository<Item> _itemRepository = itemRepository;

    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    // GET /items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()

    {
        var items = (await _itemRepository.GetAllAsync())
            .Select(item => item.AsDto());

        return Ok(items);
    }

    // GET /items/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item is null) return NotFound();

        return item.AsDto();
    }

    // POST /items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
    {
        var item = createItemDto.AsEntity();

        await _itemRepository.CreateAsync(item);
        await _publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));
        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
    }

    // PUT /items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
    {
        if (id != updateItemDto.Id) return BadRequest();

        var existingItem = await _itemRepository.GetByIdAsync(id);
        if (existingItem is null) return NotFound();

        existingItem.Name = updateItemDto.Name;
        existingItem.Description = updateItemDto.Description;
        existingItem.Price = updateItemDto.Price;

        await _itemRepository.UpdateAsync(existingItem);
        await _publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));
        return NoContent();
    }

    // DELETE /items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var existingItem = await _itemRepository.GetByIdAsync(id);
        if (existingItem is null) return NotFound();

        await _itemRepository.RemoveAsync(existingItem.Id);
        await _publishEndpoint.Publish(new CatalogItemDeleted(existingItem.Id));
        return NoContent();
    }
}