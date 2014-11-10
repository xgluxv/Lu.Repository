#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity;

#endregion

namespace Lu.Repository
{
    public sealed class RepositoryQuery<TEntity> : IRepositoryQuery<TEntity> where TEntity : EntityBase
    {
        private readonly List<Expression<Func<TEntity, object>>> _includeProperties;
        private readonly Repository<TEntity> _repository;
        private Expression<Func<TEntity, bool>> _filter;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderByQuerable;
        private int? _page;
        private int? _pageSize;

        public RepositoryQuery(Repository<TEntity> repository)
        {
            _repository = repository;
            _includeProperties = new List<Expression<Func<TEntity, object>>>();
        }

        public RepositoryQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
        {
            _filter = filter;
            return this;
        }

        public RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderByQuerable = orderBy;
            return this;
        }

        public RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includeProperties.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> GetPage(int page, int pageSize, out int totalCount)
        {
            _page = page;
            _pageSize = pageSize;
            totalCount = _repository.Get(_filter).Count();

            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
        }
        public async Task<Tuple<int, IEnumerable<TEntity>>> GetPageAsync(int page, int pageSize)
        {
            _page = page;
            _pageSize = pageSize;
            var totalCount = await _repository.Get(_filter).CountAsync();
            var result=await _repository.GetAsync(_filter, _orderByQuerable, _includeProperties, _page, _pageSize);
            return new Tuple<int, IEnumerable<TEntity>>(totalCount, result);

        }

        public IQueryable<TEntity> Get(bool tracking=true)
        {
            return _repository.Get(_filter, _orderByQuerable, _includeProperties, _page, _pageSize,tracking);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(bool tracking = true)
        {
            return await _repository.GetAsync(_filter, _orderByQuerable, _includeProperties, _page, _pageSize,tracking);
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _repository.SqlQuery(query, parameters).AsQueryable();
        }
    }
}