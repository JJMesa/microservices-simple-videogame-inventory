using System.Linq.Expressions;
using Play.Common.Entities;

namespace Play.Common.Repositories;

public interface IRepository<T> where T : IEntity
{
    Task<IReadOnlyCollection<T>> GetAllAsync();

    Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> filter);

    Task<T?> GetByIdAsync(Guid id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task RemoveAsync(Guid id);
}