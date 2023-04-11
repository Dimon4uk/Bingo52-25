using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BingoDAL.EntityFramework
{
    public class BaseBingoRepository<TEntity> : IBingoRepository<TEntity> where TEntity : class
    {
        protected BingoDbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected BaseBingoRepository(BingoDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public Task<TEntity?> Get(
            Expression<Func<TEntity, bool>> filterPredicate, bool asNoTracking = true)
        {
            IQueryable<TEntity> query = DbSet;
            if (filterPredicate != null)
            {
                query = query.Where(filterPredicate);
            }

            if (asNoTracking)
                return query.AsNoTracking().FirstOrDefaultAsync();
            else
                return query.FirstOrDefaultAsync();
        }

        public Task<TEntity> Add(TEntity entity)
        {
             Context.Add(entity);
            return Task.FromResult(entity);
        }
        public virtual Task Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            return Task.CompletedTask;
        }
        public Task<int> SaveChanges()
        {
            return Task.FromResult(Context.SaveChanges());
        }
        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

    }
}
