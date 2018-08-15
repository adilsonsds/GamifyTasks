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

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public IEnumerable<T> Get()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Remove(int id)
        {
            var type = GetById(id);
            Context.Set<T>().Remove(type);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}