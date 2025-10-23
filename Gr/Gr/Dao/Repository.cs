using Gr.Data;
using Microsoft.EntityFrameworkCore;

namespace Gr.Dao
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public Repository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<T>> GetAllAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await using var context = await _factory.CreateDbContextAsync();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await using var context = await _factory.CreateDbContextAsync();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
