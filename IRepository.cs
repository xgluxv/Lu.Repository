using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lu.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Guid InstanceId { get; }
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        //Task<int> DeleteBatchAndCommitAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<int> UpdateBatchAndCommitAsync(Expression<Func<TEntity, bool>> filterPredicate,Expression<Func<TEntity, TEntity>> updatePredicate);
        IRepositoryQuery<TEntity> Query();
    }
}