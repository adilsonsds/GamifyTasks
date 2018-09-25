using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Repositories;

namespace Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GamifyTasksContext Context;

        public Repository(GamifyTasksContext context)
        {
            Context = context;
        }

        public IQueryable<T> Queryable()
        {
            return Context.Set<T>().AsQueryable<T>();
        }

        public IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Queryable().Where(predicate);
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
            Save();
        }

        public void Remove(int id)
        {
            var type = GetById(id);
            Context.Set<T>().Remove(type);
            Save();
        }

        private void Save()
        {
            Context.SaveChanges();
        }
    }
}