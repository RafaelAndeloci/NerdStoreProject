using NerdStore.Core.DomainObjects;

namespace NerdStore.Core.Data;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid entity);
}