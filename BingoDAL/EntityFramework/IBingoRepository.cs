using BingoDAL.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BingoDAL.EntityFramework
{
    public interface IBingoRepository<TEntity>
    {
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> filterPredicate, bool asNoTracking = true);

        Task<TEntity> Add(TEntity entity);

        Task Delete(TEntity entity);
        Task<int> SaveChangesAsync();
        Task<int> SaveChanges();
        Task Update(TEntity entity);

    }
}
