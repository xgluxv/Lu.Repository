using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lu.Repository
{
    public interface IRepositoryQuery<TEntity> where TEntity : EntityBase
    {
        RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter);
        RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount);
        Task<Tuple<int, IEnumerable<TEntity>>> GetPageAsync(int page, int pageSize);
        IQueryable<TEntity> Get(bool Tracking=true);
        Task<IEnumerable<TEntity>> GetAsync(bool Tracking = true);

        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}