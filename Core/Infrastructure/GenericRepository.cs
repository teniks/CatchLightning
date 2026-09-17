using CatchLightning.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatchLightning.Core.Infrastructure
{
    public class GenericRepository<T> : IDisposable, IFindByName<T>
        where T : class, IEntity
    {
        protected DbContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIDAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetPartFromAsync(int count, int skip = 0)
        {
            return await Task.FromResult(dbSet.Skip(skip).Take(count));
        }

        public async Task<T?> GetByNameAsync(string name)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Name.Contains(name));
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
