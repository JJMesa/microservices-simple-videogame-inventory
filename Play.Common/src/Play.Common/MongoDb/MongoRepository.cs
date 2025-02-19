using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common.Entities;
using Play.Common.Repositories;

namespace Play.Common.MongoDb;

public class MongoRepository<T>(IMongoDatabase database, string collectionName) : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> dbCollection = database.GetCollection<T>(collectionName);
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

    public async Task<IReadOnlyCollection<T>> GetAllAsync() =>
        await dbCollection.Find(filterBuilder.Empty).ToListAsync();

    public async Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> filter) =>
        await dbCollection.Find(filter).ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await dbCollection.Find(filterBuilder.Eq(item => item.Id, id)).FirstOrDefaultAsync();

    public async Task CreateAsync(T item) =>
        await dbCollection.InsertOneAsync(item);

    public async Task UpdateAsync(T item) =>
        await dbCollection.ReplaceOneAsync(filterBuilder.Eq(existingItem => existingItem.Id, item.Id), item);

    public async Task RemoveAsync(Guid id) =>
        await dbCollection.DeleteOneAsync(filterBuilder.Eq(item => item.Id, id));
}