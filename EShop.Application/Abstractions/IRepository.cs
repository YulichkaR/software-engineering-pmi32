using EShop.Domain.Abstractions;

namespace EShop.Application.Abstractions;

public interface IRepository<in TKey, TEntity>
{
    Task<TEntity?> GetById(TKey id);
    Task<List<TEntity>> GetAllBySpecification(Specification<TEntity> spec);
    Task<TEntity?> GetBySpecification(Specification<TEntity> spec);
    Task<List<TEntity>> GetAll();
    Task<TEntity> Create(TEntity entity);
    Task<bool> AnyAsync(TKey id);
    Task Update(TEntity entity);
    Task Delete(TKey id);
    Task<int> CountAsync();
}