using Goreu.Entities;
using System.Linq.Expressions;

namespace Goreu.Repositories.Interface
{
   public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<ICollection<TEntity>> GetAsync();
        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, Tkey>> orderBy);
        Task<TEntity?> GetAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task UpdateAsync();
        Task DeleteAsync(int id);
        Task FinalizeAsync(int id);
        Task InitializeAsync(int id);
    }
}
