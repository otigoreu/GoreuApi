using Goreu.Entities;
using Goreu.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Goreu.Repositories.Implementation
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {

        protected readonly DbContext context;

        public RepositoryBase(DbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync<Tkey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, Tkey>> orderBy)
        {
            return await context.Set<TEntity>().Where(predicate).OrderBy(orderBy).AsNoTracking().ToListAsync();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task UpdateAsync()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var item = await context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (item is not null)
            {
                context.Set<TEntity>().Remove(item);
                await context.SaveChangesAsync();
            }
            else
                throw new InvalidOperationException($"No se encontro el registro con id {id}");
        }

        public virtual async Task FinalizeAsync(int id)
        {
            var item = await context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = false;
            await context.SaveChangesAsync();
        }

        public virtual async Task InitializeAsync(int id)
        {
            var item = await context.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == id); // sin AsNoTracking

            if (item is null)
                throw new InvalidOperationException($"No se encontró el registro con id {id}");

            item.Estado = true;
            await context.SaveChangesAsync();
        }
    }
}
