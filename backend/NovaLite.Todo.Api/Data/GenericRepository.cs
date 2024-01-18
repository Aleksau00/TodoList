using Microsoft.EntityFrameworkCore;

namespace NovaLite.Todo.Api.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;

        protected GenericRepository(ApplicationDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public async Task DeleteByIdAsync(Guid guid)
        {
            var entity = await GetByIdAsync(guid);
            if (entity == null)
            {
                return;
            }
            DbSet.Remove(entity);
        }

        public Task UpdateAsync(T entity)
        {
            var entityUpdate = DbSet.Update(entity);
            return Task.FromResult(entityUpdate);
        }
    }
}
