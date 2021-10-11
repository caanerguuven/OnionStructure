using OnionStructure.Domain.Models.Base;
using OnionStructure.Repositories.Repository.Abstract;
using OnionStructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnionStructure.Repositories.Repository.Concrete
{
    public class AppRepository<TEntity,TId> : IAppRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<TEntity> _entities;

        public AppRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<TEntity>();
        }

        public Task<TEntity> Get(TId Id)
        {
            return _entities.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }

        public Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity,object>> include)
        {
            return _entities.Include(include).FirstOrDefaultAsync(predicate);
        }

        public Task<List<TEntity>> GetAll()
        {
            return _entities.ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity Not Found");
            }

            await _entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity Not Found");
            }

            _entities.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity Not Found");
            }

            _entities.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public Task<int> Execute(string command,object[] parameters)
        {
            return _appDbContext.Database.ExecuteSqlRawAsync(command, parameters);
        }
    }
}
