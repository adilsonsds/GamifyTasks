using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infra.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GamifyTasksContext Context;

        public Repository(GamifyTasksContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Queryable()
        {
            return Context.Set<TEntity>().AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Queryable().Where(predicate);
        }

        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Save();
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Save();
        }

        public void Remove(int id)
        {
            var type = GetById(id);
            Context.Set<TEntity>().Remove(type);
            Save();
        }

        private void Save()
        {
            Context.SaveChanges();
        }
    }
}