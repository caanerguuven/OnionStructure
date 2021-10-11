using OnionStructure.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnionStructure.Repositories.Repository.Abstract
{
    public interface IAppRepository<TEntity, TId> where TEntity : class,IEntity<TId>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(TId Id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> include);
        Task Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveChanges();

        Task<int> Execute(string procName, object[] parameters);
    }
}
