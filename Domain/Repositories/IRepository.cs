using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories
{
    public interface IRepository<T> where T : class
    {

        IQueryable<T> Queryable();

        IQueryable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        T GetById(int id);

        IEnumerable<T> GetAll();
        
        void Add(T entity);

        void Update(T entity);

        void Remove(int id);

    }
}